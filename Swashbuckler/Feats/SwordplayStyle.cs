using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Swashbuckler.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Feats
{
    internal class SwordplayStyle
    {
        internal static BlueprintBuff swordstyle_buff;

        internal static WeaponCategory[] categories = new WeaponCategory[] { WeaponCategory.BastardSword, WeaponCategory.Dagger, WeaponCategory.DoubleSword, WeaponCategory.DuelingSword, WeaponCategory.ElvenCurvedBlade, WeaponCategory.Estoc, WeaponCategory.Falcata, WeaponCategory.Falchion, WeaponCategory.Greatsword, WeaponCategory.Kama, WeaponCategory.Kukri, WeaponCategory.Longsword, WeaponCategory.Rapier, WeaponCategory.Sai, WeaponCategory.Scimitar, WeaponCategory.Shortsword, WeaponCategory.Starknife, WeaponCategory.Scythe, WeaponCategory.Sickle };

        internal const string SwordFeature = "SwordplayStyleFeature";
        internal const string SwordFeatureGuid = "69F13A11-3754-4FE5-98A1-C13B08AE9EFA";
        internal const string SwordDisplayName = "Swordplay.Name";
        internal const string SwordDescription = "Swordplay.Description";

        internal const string SwordUpsetFeature = "SwordplayUpsetFeature";
        internal const string SwordUpsetFeatureGuid = "D9EDA9D8-C3FD-4C6A-A1C4-D9F548750F03";
        internal const string SwordUpsetDisplayName = "SwordplayUpset.Name";
        internal const string SwordUpsetDescription = "SwordplayUpset.Description";

        internal const string SwordAbility = "SwordplayStyleAbility";
        internal const string SwordAbilityGuid = "8203DC71-99F0-4F5D-BBA8-C9E2D9CD3EE1";

        internal const string SwordBuff = "SwordplayStyleBuff";
        internal const string SwordBuffGuid = "8B0709F3-A68D-4E3A-9C87-E3545B2A79AD";
        internal static void Configure()
        {
            swordstyle_buff = BuffConfigurator.New(SwordBuff, SwordBuffGuid)
                .SetDisplayName(SwordDisplayName)
                .SetDescription(SwordDescription)
                .AddNotDispelable()
                .AddComponent<SwordplayStyleACBuff>()
                .AddComponent<SwordplayStyleABBuff>()
                .Configure();

            var swordstyle_ability = ActivatableAbilityConfigurator.New(SwordAbility, SwordAbilityGuid)
                .SetDisplayName(SwordDisplayName)
                .SetDescription(SwordDescription)
                .SetIcon(FeatureRefs.DuelingMastery.Reference.Get().Icon)
                .SetBuff(swordstyle_buff)
                .SetGroup(ActivatableAbilityGroup.CombatStyle)
                .SetDeactivateImmediately()
                .Configure();

            var swordstyle_feat = FeatureConfigurator.New(SwordFeature, SwordFeatureGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SwordDisplayName)
                .SetDescription(SwordDescription)
                .SetIcon(FeatureRefs.DuelingMastery.Reference.Get().Icon)
                .AddFacts(new() { swordstyle_ability })
                .AddPrerequisiteFeature(FeatureRefs.CombatExpertiseFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 3);

            var swordupset_feat = FeatureConfigurator.New(SwordUpsetFeature, SwordUpsetFeatureGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SwordUpsetDisplayName)
                .SetDescription(SwordUpsetDescription)
                .SetIcon(FeatureRefs.DuelingMastery.Reference.Get().Icon)
                .AddPrerequisiteFeature(FeatureRefs.CombatExpertiseFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 5)
                .AddPrerequisiteFeature(FeintFeats.feint_feat)
                .AddComponent<SwordplayUpset>();

            foreach (var c in categories)
            {
                swordstyle_feat.AddPrerequisiteParametrizedWeaponFeature(ParametrizedFeatureRefs.WeaponFocus.Reference.Get(), c, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any);

                swordupset_feat.AddPrerequisiteParametrizedWeaponFeature(ParametrizedFeatureRefs.WeaponFocus.Reference.Get(), c, group: Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.Any);
            }

            var swordstyle_feat_conf = swordstyle_feat.Configure();
            swordupset_feat.AddPrerequisiteFeature(swordstyle_feat_conf).Configure();
        }
    }
}
