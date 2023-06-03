using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class DancersBloom : UnitFactComponentDelegate, IUnitAbilityResourceHandler
    {
        public void HandleAbilityResourceChange(UnitEntityData unit, UnitAbilityResource resource, int oldAmount)
        {
            if (unit != Owner || resource.Blueprint != Swashbuckler.panache_resource)
                return;

            if (unit.HasFact(Feats.SpringAttack.springAttackReaping))
                return;

            if (unit.Resources.GetResourceAmount(resource.Blueprint) > 0)
                unit.AddFact(Feats.SpringAttack.springAttackReapingBuff);
            else
                unit.RemoveFact(Feats.SpringAttack.springAttackReapingBuff);
        }
    }
}
