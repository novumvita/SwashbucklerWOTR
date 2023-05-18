using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class SwashbucklerInitiative : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleInitiativeRoll>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleInitiativeRoll evt)
        {
            if (evt.Initiator.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;

            evt.AddTemporaryModifier(Owner.Stats.Initiative.AddModifier(2, base.Fact, ModifierDescriptor.UntypedStackable));
        }

        public void OnEventDidTrigger(RuleInitiativeRoll evt)
        {
        }
    }
}
