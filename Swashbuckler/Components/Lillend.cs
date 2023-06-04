using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Swashbuckler.Utilities;
using System.Linq;

namespace Swashbuckler.Components
{
    internal class Lillend : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackWithWeapon>, ITargetRulebookSubscriber
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("Lillend");

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.AttackRoll.IsHit || Owner.CombatState.EngagedBy.Count < 2)
                return;

            var target = Owner.CombatState.EngagedBy.Where(c => c != evt.Initiator).First();

            if (evt.AttackRoll.Roll >= target.Stats.AC)
            {
                var rule = new RuleDealDamage(evt.Initiator, target, evt.CreateDamage(true));
                Context.TriggerRule(rule);
            }

            int cost = 2;

            if (Owner.HasFact(Swashbuckler.abundant_panache_feature))
            {
                cost -= 1;
            }

            var attackerPos = evt.Initiator.Position;
            var casterPos = evt.Target.Position;

            evt.Initiator.CombatState.PreventAttacksOfOpporunityNextFrame = true;
            evt.Target.CombatState.PreventAttacksOfOpporunityNextFrame = true;
            target.CombatState.PreventAttacksOfOpporunityNextFrame = true;

            evt.Initiator.Position = casterPos;
            evt.Target.Position = attackerPos;

            Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, cost);
        }
    }
}
