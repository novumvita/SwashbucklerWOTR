using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class KipUp : UnitFactComponentDelegate, IUnitAbilityResourceHandler
    {
        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            if (unit != Owner || resource.Blueprint != Swashbuckler.panache_resource)
                return;

            if (unit.Resources.GetResourceAmount(resource.Blueprint) > 0)
                unit.State.Features.GetUpWithoutAttackOfOpportunity.Retain();
            else
                unit.State.Features.GetUpWithoutAttackOfOpportunity.Release();
        }
    }
}
