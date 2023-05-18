using BlueprintCore.Actions.Builder;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Swashbuckler.Utilities;

namespace Swashbuckler.Components
{
    internal class MenacingSwordplay : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("Menacing");
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (evt.Initiator.CombatState.Cooldown.SwiftAction != 0.0f || !SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator) || !evt.IsHit || evt.Initiator.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;

            Logger.Log("Attempting Menacing");
            evt.Initiator.SpendAction(UnitCommand.CommandType.Swift, false, 0);
            base.Fact.RunActionInContext(ActionsBuilder.New().Add<Demoralize>().Build(), evt.Target);
            Logger.Log("Attempted Menacing");
        }
    }
}
