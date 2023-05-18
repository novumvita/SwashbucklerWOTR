using Kingmaker;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class ParryAndRiposte : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber
    {
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
                    Game.Instance.CombatEngagementController.ForceAttackOfOpportunity(Owner, evt.Initiator);

                Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, 1);
            }
        }
    }
}
