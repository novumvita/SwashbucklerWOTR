using Kingmaker;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Swashbuckler.Utilities;
using System.Linq;
using UnityEngine.Assertions.Must;

namespace Swashbuckler.Components
{
    internal class ParryAndRiposte : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("Parry");

        private bool willSpend = false;
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (!evt.Weapon.Blueprint.IsMelee || evt.Parry != null || !Owner.IsReach(evt.Target, Owner.Body.PrimaryHand))
                return;

            if (Owner.Descriptor.HasFact(Archetypes.Azatariel.whimsical_feat))
            {
                evt.AddTemporaryModifier(Owner.Stats.AdditionalAttackBonus.AddModifier(Owner.Stats.Charisma.Bonus, Fact));
            }

            evt.TryParry(Owner, Owner.Body.PrimaryHand.Weapon, 2 * (evt.Initiator.Descriptor.State.Size - Owner.State.Size));

            willSpend = true;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (willSpend)
            {
                willSpend = false;

                if (!evt.Parry.IsTriggered)
                    return;

                if (evt.Result == AttackResult.Parried && evt.Target.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) >= 1)
                {
                    if (!Owner.Descriptor.HasFact(Archetypes.Azatariel.whimsical_buff) || !Owner.CombatState.IsFlanked)
                    {
                        Logger.Log("Riposte");
                        Game.Instance.CombatEngagementController.ForceAttackOfOpportunity(Owner, evt.Initiator);
                    }
                    else
                    {
                        Logger.Log("Whimsy");
                        var target = Owner.CombatState.EngagedBy.Where(c => c != evt.Initiator).First();
                        var og_rule = evt.Reason.Rule as RuleAttackWithWeapon;

                        if (evt.Roll >= target.Stats.AC)
                        {
                            var rule = new RuleDealDamage(evt.Initiator, target, og_rule.CreateDamage(true));
                            Context.TriggerRule(rule);
                        }
                        Logger.Log("Damage rule triggered");

                    }
                }
                Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, 1);
            }
        }
    }
}
