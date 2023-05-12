using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Patches
{
    [HarmonyPatch(typeof(UnitEntityView))]
    [HarmonyPatch("LeaveProneState", MethodType.Normal)]
    class Patch_UnitEntityView_LeaveProneState
    {
        static BlueprintBuff kip_up_buff = Swashbuckler.kip_buff;
        static BlueprintAbilityResource resource = Swashbuckler.panache_resource;

        static public void Postfix(UnitEntityView __instance)
        {
            var unit = __instance.EntityData;

            if (unit.Descriptor.HasFact(kip_up_buff) && unit.Descriptor.Resources.GetResourceAmount(resource) > 0 && unit.CombatState.Cooldown.SwiftAction == 0.0f)
            {
                unit.CombatState.Cooldown.SwiftAction = 6.0f;
                unit.Descriptor.Resources.Spend(resource, 1);
                unit.CombatState.Cooldown.MoveAction = 0.0f;
            }
        }
    }
}
