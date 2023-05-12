using BlueprintCore.Blueprints.References;
using Kingmaker.Controllers.Projectiles;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Entities;

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
