using HarmonyLib;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using Swashbuckler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Patches
{
    internal class ExpandedActivatableAbilityGroup
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ExpandedActivatableAbilityGroup));

        internal const ActivatableAbilityGroup BleedingWound = (ActivatableAbilityGroup)1350;

        [HarmonyPatch(typeof(UnitPartActivatableAbility))]
        static class UnitPartActivatableAbility_Patch
        {
            [HarmonyPatch(nameof(UnitPartActivatableAbility.GetGroupSize)), HarmonyPrefix]
            static bool GetGroupSize(ActivatableAbilityGroup group, ref int __result)
            {
                try
                {
                    if (group == BleedingWound)
                    {
                        Logger.Verbose(() => "Returning group size for BleedingWound");
                        __result = 1;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.LogException("UnitPartActivatableAbility_Patch.GetGroupSize", e);
                }
                return true;
            }
        }
    }
}
