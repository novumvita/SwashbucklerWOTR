using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist.Properties;

namespace Swashbuckler.Patches
{
    [HarmonyPatch(typeof(FightingDefensivelyAttackPenaltyProperty))]
    [HarmonyPatch("GetBaseValue", MethodType.Normal)]
    class Patch_FightingDefensivelyAttackPenaltyProperty_GetInt
    {
        static BlueprintBuff dizzying_defense_buff = Swashbuckler.dizzying_defense_buff;

        static public void Postfix(FightingDefensivelyAttackPenaltyProperty __instance, UnitEntityData unit, ref int __result)
        {
            var fact = unit.Descriptor.GetFact(dizzying_defense_buff);
            if (fact != null)
            {
                __result -= 2;
            }
        }
    }

    [HarmonyPatch(typeof(FightingDefensivelyACBonusProperty))]
    [HarmonyPatch("GetBaseValue", MethodType.Normal)]
    class Patch_FightingDefensivelyACBonusProperty_GetInt
    {
        static BlueprintBuff dizzying_defense_buff = Swashbuckler.dizzying_defense_buff;

        static public void Postfix(FightingDefensivelyACBonusProperty __instance, UnitEntityData unit, ref int __result)
        {
            var fact = unit.Descriptor.GetFact(dizzying_defense_buff);
            if (fact != null)
            {
                __result += 2;
            }
        }
    }
}
