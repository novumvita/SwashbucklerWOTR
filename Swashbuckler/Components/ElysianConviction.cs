using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Swashbuckler.Archetypes;

namespace Swashbuckler.Components
{
    internal class ElysianConviction : UnitFactComponentDelegate, IUnitAbilityResourceHandler
    {
        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            if (unit != Owner || resource.Blueprint != Swashbuckler.panache_resource)
                return;

            if (unit.Resources.GetResourceAmount(resource.Blueprint) > 0)
                unit.AddFact(Azatariel.elysian_buff);
            else if (unit.HasFact(Azatariel.elysian_buff))
                unit.RemoveFact(Azatariel.elysian_buff);
        }
    }
}
