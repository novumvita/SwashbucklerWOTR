using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Swashbuckler.Archetypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
