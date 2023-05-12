using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Swashbuckler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    public class AbilityTargetNotImmuneToPrecision : BlueprintComponent, IAbilityTargetRestriction
    {
        public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
        {
            return "Target is immune to precision damage";
        }
        public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
        {
            return (!(target.Unit.Descriptor.Progression.Features.Enumerable.Any(f => f.Blueprint.GetComponent<AddImmunityToPrecisionDamage>() != null) || target.Unit.Descriptor.Buffs.Enumerable.Any(f => f.Blueprint.GetComponent<AddImmunityToPrecisionDamage>() != null)));
        }
    }

    public class AbilityTargetNotImmuneToCritical : BlueprintComponent, IAbilityTargetRestriction
    {
        public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
        {
            return "Target is immune to criticals";
        }
        public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
        {
            return (!(target.Unit.Descriptor.Progression.Features.Enumerable.Any(f => f.Blueprint.GetComponent<AddImmunityToCriticalHits>() != null) || target.Unit.Descriptor.Buffs.Enumerable.Any(f => f.Blueprint.GetComponent<AddImmunityToCriticalHits>() != null)));
        }
    }

    public class AbilityTargetNotImmuneToMindAffecting : BlueprintComponent, IAbilityTargetRestriction
    {
        public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
        {
            return "Target is immune to mind-affecting effects";
        }
        public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
        {
            return (!target.Unit.Descriptor.HasFact(FeatureRefs.ImmunityToMindAffecting.Reference.Get()));
        }
    }

    public class AbilityTargetNotImmuneToProne : BlueprintComponent, IAbilityTargetRestriction
    {
        public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
        {
            return "Target cannot be knocked prone";
        }
        public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
        {
            return (!(target.Unit.Descriptor.HasFact(FeatureRefs.TripImmune.Reference.Get()) || target.Unit.Descriptor.HasFact(FeatureRefs.TripDefenseFourLegs.Reference.Get()) || target.Unit.Descriptor.HasFact(FeatureRefs.TripDefenseEightLegs.Reference.Get())));
        }
    }
}