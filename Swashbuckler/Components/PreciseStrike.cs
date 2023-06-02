using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Parts;

namespace Swashbuckler.Components
{
    internal class SwashbucklerPreciseStrike : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (Owner.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1 || !SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator) || (Owner.Body.SecondaryHand.HasWeapon && base.Owner.Body.SecondaryHand.MaybeWeapon != base.Owner.Body.EmptyHandWeapon) || (Owner.Body.SecondaryHand.HasShield && Owner.Body.SecondaryHand.MaybeShield.ArmorComponent.Blueprint.ProficiencyGroup != Kingmaker.Blueprints.Items.Armors.ArmorProficiencyGroup.Buckler))
                return;

            int swash_level = Owner.Progression.GetClassLevel(Swashbuckler.swash_class);
            evt.PreciseStrike += Owner.Descriptor.HasFact(Swashbuckler.precise_strike_buff) ? 2 * swash_level : swash_level;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }
    }

    internal class WarriorPoetStrike : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
        private bool triggered;

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.IsFirstAttack)
                triggered = false;

        }
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            if (Owner.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;

            int bonus = Owner.Progression.GetClassLevel(Swashbuckler.swash_class);

            if (IsSuitable(evt) && triggered)
                evt.AddDamageModifier(bonus/2, Fact);
            else
                evt.AddDamageModifier(bonus, Fact);
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.AttackRoll.IsHit)
                triggered = true;
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }

        private bool IsSuitable(RuleCalculateWeaponStats evt)
        {
            var weapon = evt.Weapon;
            var ruleCalculateAttackBonus = new RuleCalculateAttackBonusWithoutTarget(evt.Initiator, weapon, 0);
            ruleCalculateAttackBonus.WeaponStats.m_Triggered = true;
            Rulebook.Trigger(ruleCalculateAttackBonus);

            return (evt.DamageBonusStat == StatType.Strength)
                && ruleCalculateAttackBonus.AttackBonusStat == StatType.Dexterity;
        }
    }
}
