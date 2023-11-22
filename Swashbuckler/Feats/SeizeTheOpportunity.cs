using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace Swashbuckler.Feats
{
    internal class SeizeTheOpportunity
    {
        private static string StOBuffName = "SeizeTheOpportunityBuff - Removed";
        private static string StOBuffGuid = "1BF9A0CC-A92A-44B7-B9AE-9AB0EA708791";

        private static string StOAbilityName = "SeizeTheOpportunityAbility - Removed";
        private static string StOAbilityGuid = "439E68E3-E797-488D-91B0-0736882F9318";

        private static string StOFeatName = "SeizeTheOpportunityFeature - Removed";
        private static string StOFeatGuid = "63CC35E0-71C4-4CD3-92EF-2DCB8B5855CF";

        internal static void Configure()
        {
            BuffConfigurator.New(StOBuffName, StOBuffGuid)
                .Configure();

            ActivatableAbilityConfigurator.New(StOAbilityName, StOAbilityGuid)
                .SetHiddenInUI()
                .Configure();

            FeatureConfigurator.New(StOFeatName, StOFeatGuid)
                .SetHideInUI()
                .SetHideInCharacterSheetAndLevelUp()
                .Configure();
        }
    }
}