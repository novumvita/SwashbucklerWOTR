using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace Swashbuckler.Components
{
    public class FeintFlatFooted : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCheckTargetFlatFooted>, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        public bool greater;

        public void OnEventAboutToTrigger(RuleCheckTargetFlatFooted evt)
        {
            var attack = Rulebook.CurrentContext.AllEvents.LastOfType<RuleAttackRoll>();

            if (attack == null)
            {
                return;
            }

            if (attack.Weapon.Blueprint.IsMelee && (greater || evt.Initiator == this.Context?.MaybeCaster))
            {
                evt.ForceFlatFooted = true;
            }
        }


        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {

        }

        public void OnEventDidTrigger(RuleCheckTargetFlatFooted evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (greater || evt.Initiator != this.Context?.MaybeCaster)
            {
                return;
            }

            this.Buff.Remove();
        }
    }

    public class ContextFeintSkillCheck : ContextAction
    {
        public bool autoSucceed;

        public ActionList Success;
        public ActionList Failure;

        static BlueprintFeature[] single_penalty_facts = new BlueprintFeature[] { FeatureRefs.DragonType.Reference.Get(), FeatureRefs.MagicalBeastType.Reference.Get() };

        static BlueprintFeature[] double_penalty_facts = new BlueprintFeature[] { FeatureRefs.AnimalType.Reference.Get() };

        static BlueprintFeature[] fail_facts = new BlueprintFeature[] { FeatureRefs.VerminType.Reference.Get(), FeatureRefs.PlantType.Reference.Get() };


        public override string GetCaption()
        {
            return "Feint check";
        }

        public override void RunAction()
        {
            int dc = 10 + this.Target.Unit.Descriptor.Stats.BaseAttackBonus.ModifiedValue + this.Target.Unit.Descriptor.Stats.Wisdom.Bonus;

            if (targetHasFactFromList(double_penalty_facts))
            {
                dc += 8;
            }
            else if (targetHasFactFromList(single_penalty_facts))
            {
                dc += 4;
            }

            var rule = new RuleSkillCheck(this.Context.MaybeCaster, StatType.CheckBluff, dc) { ShowAnyway = true };

            if (autoSucceed)
                rule.EnsureSuccess = true;

            if (targetHasFactFromList(fail_facts))
                rule.EnsureSuccess = false;

            if (this.Context.TriggerRule(rule).Success)
                this.Success.Run();
            else
                this.Failure.Run();
        }

        private bool targetHasFactFromList(params BlueprintFeature[] facts)
        {
            foreach (var f in facts)
            {
                if (this.Target.Unit.Descriptor.HasFact(f))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
