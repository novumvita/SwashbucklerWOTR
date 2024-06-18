using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace Swashbuckler.Feats
{
    internal class StyleMastery
    {
        private const string StyleName = "StyleMastery";
        private const string StyleGuid = "01567B8E-1FE6-4ABF-84CA-087F2C443BAC";
        private const string StyleDisplayName = "StyleMastery.Name";
        private const string StyleDescription = "StyleMastery.Description";
        internal static void Configure()
        {
            FeatureConfigurator.New(StyleName, StyleGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(StyleDisplayName)
                .SetDescription(StyleDescription)
                .SetIcon(FeatureRefs.Dodge.Reference.Get().Icon)
                .AddIncreaseActivatableAbilityGroupSize(ActivatableAbilityGroup.CombatStyle)
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 6)
                .Configure();
        }
    }
}
