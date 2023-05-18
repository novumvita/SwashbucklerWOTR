using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities.Components.Base;

namespace Swashbuckler.Components
{
    public class AbilityCasterSwashbucklerWeaponCheck : BlueprintComponent, IAbilityCasterRestriction
    {
        public string GetAbilityCasterRestrictionUIText()
        {
            return "Requires swashbuckler weapon";
        }

        public bool IsCasterRestrictionPassed(UnitEntityData caster)
        {
            return (SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(caster.Body.PrimaryHand.Weapon.Blueprint, caster.Descriptor) || SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(caster.Body.SecondaryHand.Weapon.Blueprint, caster.Descriptor));
        }
    }
    public class AbilityCasterHasAtLeastOnePanache : BlueprintComponent, IAbilityCasterRestriction
    {
        public string GetAbilityCasterRestrictionUIText()
        {
            return "Requires at least 1 panache point";
        }

        public bool IsCasterRestrictionPassed(UnitEntityData caster)
        {
            return (caster.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) > 0);
        }
    }
}
