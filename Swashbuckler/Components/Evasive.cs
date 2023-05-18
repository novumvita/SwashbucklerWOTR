using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class Evasive : UnitFactComponentDelegate, IUnitAbilityResourceHandler
    {
        private bool isActive = false;
        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {

            if (unit != Owner || resource.Blueprint != Swashbuckler.panache_resource)
                return;

            if (unit.Resources.GetResourceAmount(resource.Blueprint) > 0 && !isActive)
            {
                isActive = true;
                unit.Descriptor.AddFact(FeatureRefs.Evasion.Reference.Get());
                unit.Descriptor.AddFact(FeatureRefs.UncannyDodge.Reference.Get());
                unit.Descriptor.AddFact(FeatureRefs.ImprovedUncannyDodge.Reference.Get());
            }
            if (unit.Resources.GetResourceAmount(resource.Blueprint) == 0 && isActive)
            {
                isActive = false;
                unit.Descriptor.RemoveFact(FeatureRefs.Evasion.Reference.Get());
                unit.Descriptor.RemoveFact(FeatureRefs.UncannyDodge.Reference.Get());
                unit.Descriptor.RemoveFact(FeatureRefs.ImprovedUncannyDodge.Reference.Get());
            }
        }
    }
}
