using BlueprintCore.Blueprints.References;
using Kingmaker;
using Kingmaker.Controllers.Projectiles;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Swashbuckler.Utilities;
using System.Linq;

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

                int cost = 1;

                if (!evt.Parry.IsTriggered)
                    return;

                if (evt.Result == AttackResult.Parried && evt.Target.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) >= 1)
                {
                    if (!Owner.Descriptor.HasFact(Archetypes.Azatariel.whimsical_buff) || Owner.CombatState.EngagedBy.Count < 2)
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

                        if (Owner.HasFact(Swashbuckler.abundant_panache_feature))
                        {
                            cost -= 1;
                        }

                    }
                }
                Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, cost);
            }
        }
    }

    internal class DodgingParry : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("DParry");

        private bool willSpend = false;
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (!evt.Weapon.Blueprint.IsMelee || evt.Parry != null || !Owner.IsReach(evt.Target, Owner.Body.PrimaryHand))
                return;

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
                    evt.Initiator.CombatState.PreventAttacksOfOpporunityNextFrame = true;
                    evt.Target.CombatState.PreventAttacksOfOpporunityNextFrame = true;

                    var displacement = (evt.Target.Position - evt.Initiator.Position).normalized;
                    var initialPos = evt.Target.Position;
                    evt.Target.Position = initialPos + displacement;

                    Game.Instance.ProjectileController.Launch(evt.Target, evt.Target, ProjectileRefs.WindProjectile00.Reference.Get(), initialPos, delegate (Projectile p)
                    {
                    });
                }
                Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, 1);
            }
        }
    }
}
