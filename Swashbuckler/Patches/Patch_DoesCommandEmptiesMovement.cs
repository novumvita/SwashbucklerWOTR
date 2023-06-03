using HarmonyLib;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Swashbuckler.Feats;

namespace Swashbuckler.Patches
{
    [HarmonyPatch(typeof(UnitCommand))]
    [HarmonyPatch(nameof(UnitCommand.DoesCommandEmptiesMovement), MethodType.Normal)]
    static class Patch_DoesCommandEmptiesMovement
    {
        static BlueprintBuff buff1 = SpringAttack.springAttackBuff1;
        static BlueprintBuff buff2 = SpringAttack.springAttackBuff2;
        static BlueprintBuff buff3 = SpringAttack.springAttackBuff3;

        static void Postfix(UnitCommand __instance, ref bool __result)
        {
            if (__instance.Executor.HasFact(buff1) || __instance.Executor.HasFact(buff2) || __instance.Executor.HasFact(buff3))
            {
                __result = false;
            }
        }
    }
}
