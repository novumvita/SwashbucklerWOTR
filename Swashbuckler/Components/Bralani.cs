using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class Bralani : UnitFactComponentDelegate, IUnitAbilityResourceHandler
    {
        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            if (unit != Owner || resource.Blueprint != Swashbuckler.panache_resource)
                return;

            if (unit.Resources.GetResourceAmount(resource.Blueprint) > 0)
                unit.State.AddConditionImmunity(UnitCondition.DifficultTerrain);
            else
                unit.State.RemoveConditionImmunity(UnitCondition.DifficultTerrain);
        }
    }
}
