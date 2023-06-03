using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace Swashbuckler.Patches
{
    [HarmonyPatch(typeof(ClassLevelGetter))]
    [HarmonyPatch(nameof(ClassLevelGetter.GetBaseValue))]
    internal class Patch_ClassLevelGetter
    {
        static void Postfix(UnitEntityData unit, ClassLevelGetter __instance, ref int __result)
        {
            if (!unit.HasFact(Swashbuckler.swash_weapon_training) && !unit.HasFact(Archetypes.InspiredBlade.rapier_training))
                return;

            if (__instance.Class != CharacterClassRefs.FighterClass.Reference.Get())
                return;

            ClassData swashData = unit.Progression.GetClassData(Swashbuckler.swash_class);

            if (swashData == null)
                return;

            int swashLevel = unit.Progression.GetClassLevel(Swashbuckler.swash_class);

            if (swashLevel > __result)
                __result = swashLevel;
        }
    }
}
