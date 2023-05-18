using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
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
}
