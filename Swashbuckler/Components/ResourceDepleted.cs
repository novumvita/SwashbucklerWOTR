using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Swashbuckler.Components
{
    public class AddAbilityResourceDepletedTrigger : UnitFactComponentDelegate, IUnitAbilityResourceHandler, IGlobalSubscriber, ISubscriber
    {
        [SerializeField]
        public BlueprintAbilityResourceReference m_Resource;

        public ActionList Action;

        public int Cost;

        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            if (oldAmount > resource.Amount && !(unit != base.Fact.Owner.Unit) && m_Resource.Is(resource.Blueprint as BlueprintAbilityResource) && resource.Amount < Cost)
            {
                (base.Fact as IFactContextOwner)?.RunActionInContext(Action);
            }
        }
    }
}
