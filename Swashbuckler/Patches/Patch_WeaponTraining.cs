using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Parts;
using Swashbuckler.Components;
using System;

namespace Swashbuckler.Patches
{
    [HarmonyPatch(typeof(UnitPartWeaponTraining))]
    [HarmonyPatch("GetWeaponRank", MethodType.Normal)]
    [HarmonyPatch(new Type[] { typeof(ItemEntityWeapon) })]
    class Patch_UnitPartWeaponTraining_GetWeaponRank
    {
        static BlueprintFeature swashbuckler_weapon_training = Swashbuckler.swash_weapon_training;
        static BlueprintFeature rapier_training = Archetypes.InspiredBlade.rapier_training;

        static public void Postfix(UnitPartWeaponTraining __instance, ItemEntityWeapon weapon, ref int __result)
        {
            if (weapon == null)
            {
                return;
            }

            var fact = __instance.Owner.GetFact(swashbuckler_weapon_training);

            if (fact != null && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(weapon.Blueprint, __instance.Owner.Unit.Descriptor))
            {
                var rank = fact.GetRank();

                if (rank > __result)
                {
                    __result = rank;
                }
            }

            fact = __instance.Owner.GetFact(rapier_training);

            if (fact != null && weapon.Blueprint.Category == WeaponCategory.Rapier)
            {
                var rank = fact.GetRank();

                if (rank > __result)
                {
                    __result = rank;
                }
            }
        }
    }

    [HarmonyPatch(typeof(BlueprintParametrizedFeature))]
    [HarmonyPatch(nameof(BlueprintParametrizedFeature.CanSelect))]
    class Patch_ParametrizedFeature_CanSelect
    {
        static BlueprintFeature swashbuckler_weapon_training = Swashbuckler.swash_weapon_training;
        static BlueprintFeature rapier_training = Archetypes.InspiredBlade.rapier_training;

        static public void Postfix(UnitDescriptor unit, LevelUpState state, FeatureSelectionState selectionState, IFeatureSelectionItem item, BlueprintParametrizedFeature __instance, ref bool __result)
        {
            if (__result)
                return;

            if (__instance != ParametrizedFeatureRefs.ImprovedCriticalMythicFeat.Reference.Get())
                return;

            var fact = unit.GetFact(swashbuckler_weapon_training);

            if (fact != null && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(item.Param.WeaponCategory.Value, unit))
            {
                __result = true;
                return;
            }

            fact = unit.GetFact(rapier_training);

            if (fact != null && item.Param.WeaponCategory.Value == WeaponCategory.Rapier)
            {
                __result = true;
                return;
            }
        }
    }
}
