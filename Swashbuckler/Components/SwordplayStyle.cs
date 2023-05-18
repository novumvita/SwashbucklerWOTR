using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Swashbuckler.Feats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class SwordplayUpset : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
        }
        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (!SwordplayStyle.categories.Contains(evt.Target.GetFirstWeapon().Blueprint.Category))
                return;
            if (!evt.Target.Descriptor.Buffs.HasFact(SwordplayStyle.swordstyle_buff))
                return;
            if (evt.IsHit)
                return;
            (this.Fact as IFactContextOwner)?.RunActionInContext(ActionsBuilder.New().Add<ContextFeintSkillCheck>(c => c.Success = FeintFeats.feint_action).Build(), evt.Initiator);
        }
    }

    internal class SwordplayStyleACBuff : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (!SwordplayStyle.categories.Contains(evt.Target.GetFirstWeapon().Blueprint.Category))
                return;
            if (!evt.Target.Descriptor.Buffs.HasFact(BuffRefs.CombatExpertiseBuff.Reference) && !evt.Target.Descriptor.Buffs.HasFact(BuffRefs.FightDefensivelyBuff.Reference))
                return;

            evt.AddTemporaryModifier(evt.Target.Stats.AC.AddModifier(1, base.Fact, ModifierDescriptor.Shield));
        }
        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }
    }

    internal class SwordplayStyleABBuff : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
        }
        public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
            if (evt.Weapon is null || !SwordplayStyle.categories.Contains(evt.Weapon.Blueprint.Category))
                return;

            if (evt.Reason.Rule is RuleAttackWithWeapon attackRule && !attackRule.IsFirstAttack)
                return;

            var combatExpertiseModifier = evt.m_ModifiableBonus?.Modifiers?
              .Where(m => m.Fact?.Blueprint == BuffRefs.CombatExpertiseBuff.Reference.Get())
              .Select(m => (Modifier?)m)
              .FirstOrDefault();

            if (combatExpertiseModifier is null)
                return;

            int bonus = combatExpertiseModifier.Value.Value;

            evt.AddModifier(-bonus, Fact);
            evt.Result -= bonus;
        }
    }
}
