using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class SwashbucklerGrace : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (!evt.RuleAttackWithWeapon.IsAttackOfOpportunity || (evt.Target.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1) || (evt.Target.Descriptor.Buffs.HasFact(BuffRefs.MobilityUseAbilityBuff.Reference)))
                return;

            RuleCalculateCMD cmdrule = new RuleCalculateCMD(evt.Target, evt.Target, CombatManeuver.None);

            RuleSkillCheck rule = new RuleSkillCheck(evt.Target, StatType.SkillMobility, Fact.MaybeContext.TriggerRule<RuleCalculateCMD>(cmdrule).Result);

            if (Fact.MaybeContext.TriggerRule<RuleSkillCheck>(rule).Success)
                evt.AutoMiss = true;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }
    }
}
