using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Swashbuckler.Components;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;

namespace Swashbuckler.Archetypes
{
    internal class InspiredBlade
    {
        static internal BlueprintArchetype inspired_blade;

        static internal BlueprintBuff inspired_buff;
        static internal BlueprintBuff inspired_crit_buff;

        static internal BlueprintFeature inspired_deeds11;
        static internal BlueprintFeature rapier_training;
        static internal BlueprintFeature rapier_mastery;
        static internal BlueprintFeature inspired_panache;
        static internal BlueprintFeature inspired_finesse;

        private const string ArchetypeName = "InspiredBlade";
        private const string ArchetypeGuid = "4A3E7900-AE35-43E1-A943-EC19C5749BDE";
        private const string ArchetypeDisplayName = "InspiredBlade.Name";
        private const string ArchetypeDescription = "InspiredBlade.Description";

        private const string InspiredPanache = "InspiredPanache";
        private const string InspiredPanacheDisplayName = "InspiredPanache.Name";
        private const string InspiredPanacheDescription = "InspiredPanache.Description";
        private const string InspiredPanacheGuid = "D4D68A07-A658-48D8-A8D0-B089816589A6";

        private const string InspiredFinesse = "InspiredFinesse";
        private const string InspiredFinesseDisplayName = "InspiredFinesse.Name";
        private const string InspiredFinesseDescription = "InspiredFinesse.Description";
        private const string InspiredFinesseGuid = "92824EE6-53AC-4CCF-A857-5167C1CBC93F";

        internal const string RTraining = "RapierTraining";
        internal const string RTrainingGuid = "6BA181E0-5A9E-4215-95EE-2E135594A641";
        internal const string RTraining2 = "RapierTraining2";
        internal const string RTraining2Guid = "F7EEEEC0-E708-4A41-8A5E-FC111EFC1F93";
        internal const string RTrainingDisplayName = "RTraining.Name";
        internal const string RTrainingDescription = "RTraining.Description";

        internal const string RMastery = "RapierMastery";
        internal const string RMasteryGuid = "F53F6AAA-A2D6-4074-A285-FAE72837B93C";
        internal const string RMasteryDisplayName = "RMastery.Name";
        internal const string RMasteryDescription = "RMastery.Description";

        private const string InspiredStrike = "InspiredStrike";
        private const string InspiredStrikeGuid = "CEE38552-BF6D-49FA-81E6-B413949BEA83";
        private const string InspiredStrikeAbility = "InspiredStrikeAbility";
        private const string InspiredStrikeAbilityGuid = "A78896C6-CAB8-41FA-84B9-58B5AF42FA6E";
        private const string InspiredStrikeCritAbility = "InspiredStrikeCritAbility";
        private const string InspiredStrikeCritAbilityGuid = "8C8BA0F2-F3F0-4F80-AF56-2B339FC64DEC";
        private const string InspiredStrikeBuff = "InspiredStrikeBuff";
        private const string InspiredStrikeBuffGuid = "77096531-6CF5-4E3E-A24B-04FFB168AEAC";
        private const string InspiredStrikeCritBuff = "InspiredStrikeCritBuff";
        private const string InspiredStrikeCritBuffGuid = "E404CF05-FEF4-48D3-902B-D035A60D3DD6";
        private const string InspiredStrikeDisplayName = "InspiredStrike.Name";
        private const string InspiredStrikeDescription = "InspiredStrike.Description";
        private const string InspiredStrikeAbilityDescription = "InspiredStrikeAbility.Description";
        private const string InspiredStrikeCritAbilityDescription = "InspiredStrikeCritAbility.Description";
        private const string InspiredStrikeCritAbilityDisplayName = "InspiredStrikeCritAbility.Name";

        private const string InspiredDeeds11 = "InspiredDeeds11";
        private const string InspiredDeeds11Guid = "100B7921-F76A-4731-B5C7-DC5BED015AF7";

        internal static void Configure()
        {
            var archetype =
              ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, Swashbuckler.swash_class)
                .SetLocalizedName(ArchetypeDisplayName)
                .SetLocalizedDescription(ArchetypeDescription);

            archetype
              .AddToRemoveFeatures(1, Swashbuckler.swash_finesse, Swashbuckler.panache_feature)
              .AddToRemoveFeatures(5, Swashbuckler.swash_weapon_training)
              .AddToRemoveFeatures(9, Swashbuckler.swash_weapon_training)
              .AddToRemoveFeatures(11, Swashbuckler.deeds11)
              .AddToRemoveFeatures(13, Swashbuckler.swash_weapon_training)
              .AddToRemoveFeatures(17, Swashbuckler.swash_weapon_training)
              .AddToRemoveFeatures(20, Swashbuckler.swash_weapon_mastery);

            inspired_deeds11 = CreateDeeds11();
            inspired_panache = CreatePanache();
            inspired_finesse = CreateFinesse();
            rapier_training = CreateWeaponTraining();
            rapier_mastery = CreateWeaponMastery();

            archetype
                .AddToAddFeatures(1, inspired_panache, inspired_finesse)
                .AddToAddFeatures(5, rapier_training)
                .AddToAddFeatures(9, rapier_training)
                .AddToAddFeatures(11, inspired_deeds11)
                .AddToAddFeatures(13, rapier_training)
                .AddToAddFeatures(17, rapier_training)
                .AddToAddFeatures(20, rapier_mastery);

            inspired_blade = archetype.Configure();
        }

        internal static BlueprintFeature CreatePanache()
        {
            return FeatureConfigurator.New(InspiredPanache, InspiredPanacheGuid)
                .SetDisplayName(InspiredPanacheDisplayName)
                .SetDescription(InspiredPanacheDescription)
                .SetIcon(FeatureRefs.Bravery.Reference.Get().Icon)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Intelligence))
                .AddAbilityResources(resource: Swashbuckler.panache_resource, restoreAmount: true)
                .AddIncreaseResourceAmountBySharedValue(resource: Swashbuckler.panache_resource, value: ContextValues.Rank())
                .AddInitiatorAttackWithWeaponTrigger(action: ActionsBuilder.New().Conditional(conditions: ConditionsBuilder.New().HasBuff(inspired_buff).HasBuff(inspired_crit_buff).Build(), ifFalse: ActionsBuilder.New().RestoreResource(Swashbuckler.panache_resource, 1)), actionsOnInitiator: true, category: WeaponCategory.Rapier, criticalHit: true)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateFinesse()
        {
            var swash_finesse = FeatureConfigurator.New(InspiredFinesse, InspiredFinesseGuid)
                .SetDisplayName(InspiredFinesseDisplayName)
                .SetDescription(InspiredFinesseDescription)
                .SetIcon(FeatureRefs.WeaponFinesse.Reference.Get().Icon)
                .AddComponent<AttackStatReplacementForRapier>()
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureRefs.WeaponFinesse.Reference.deserializedGuid } })
                .AddParametrizedFeatures(new AddParametrizedFeatures.FeatureData[] { new AddParametrizedFeatures.FeatureData { m_Feature = ParametrizedFeatureRefs.WeaponFocus.Reference.GetBlueprint().ToReference<BlueprintParametrizedFeatureReference>(), ParamWeaponCategory = WeaponCategory.Rapier } })
                .SetIsClassFeature()
                .Configure();

            FeatureConfigurator.For(FeatureRefs.WeaponFinesse)
                .AddRecommendationNoFeatFromGroup(new() { swash_finesse })
                .Configure();

            ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.WeaponFocus)
                .AddRecommendationNoFeatFromGroup(new() { swash_finesse })
                .Configure();

            return swash_finesse;
        }

        internal static BlueprintFeature CreateWeaponTraining()
        {
            var rapier_training = FeatureConfigurator.New(RTraining, RTrainingGuid)
                .SetDisplayName(RTrainingDisplayName)
                .SetDescription(RTrainingDescription)
                .SetIcon(ActivatableAbilityRefs.ArcaneWeaponFrostChoice.Reference.Get().Icon)
                .AddComponent<ImprovedCriticalOnWieldingRapier>()
                .AddWeaponTraining()
                .AddWeaponCategoryAttackBonus(1, WeaponCategory.Rapier, ModifierDescriptor.WeaponTraining)
                .AddComponent<RapierDamageBonus>()
                .AddContextRankConfig(ContextRankConfigs.FeatureRank(RTraining))
                .SetReapplyOnLevelUp()
                .SetRanks(10)
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureSelectionRefs.WeaponTrainingSelection.Reference.deserializedGuid } })
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = ParametrizedFeatureRefs.ImprovedCritical.Reference.deserializedGuid } })
                .AddToGroups(FeatureGroup.WeaponTraining)
                .SkipAddToSelections()
                .SetIsClassFeature()
                .Configure();

            FeatureConfigurator.New(RTraining2, RTraining2Guid)
                .SetHideInUI()
                .SetHideInCharacterSheetAndLevelUp()
                .Configure();

            ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.ImprovedCritical)
                .AddRecommendationNoFeatFromGroup(new() { rapier_training })
                .Configure();

            return rapier_training;
        }

        internal static BlueprintFeature CreateWeaponMastery()
        {
            return FeatureConfigurator.New(RMastery, RMasteryGuid)
                .SetDisplayName(RMasteryDisplayName)
                .SetDescription(RMasteryDescription)
                .SetIcon(ActivatableAbilityRefs.ArcaneWeaponAnarchicChoice.Reference.Get().Icon)
                .AddComponent<CritAutoconfirmWithRapier>()
                .AddComponent<IncreasedCriticalMultiplierWithRapier>()
                .AddComponent<IncreasedCriticalEdgeWithRapier>()
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateInspiredStrike()
        {
            inspired_buff = BuffConfigurator.New(InspiredStrikeBuff, InspiredStrikeBuffGuid)
                .SetDisplayName(InspiredStrikeDisplayName)
                .SetDescription(InspiredStrikeAbilityDescription)
                .SetIcon(FeatureRefs.SacredWeaponEnchantFeature.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .AddComponent<InspiredStrike>()
                .Configure();

            var inspired_ability = ActivatableAbilityConfigurator.New(InspiredStrikeAbility, InspiredStrikeAbilityGuid)
                .SetDisplayName(InspiredStrikeDisplayName)
                .SetDescription(InspiredStrikeAbilityDescription)
                .SetIcon(FeatureRefs.SacredWeaponEnchantFeature.Reference.Get().Icon)
                .SetBuff(inspired_buff)
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ResourceSpendType.Never)
                .SetDeactivateImmediately()
                .Configure();

            inspired_crit_buff = BuffConfigurator.New(InspiredStrikeCritBuff, InspiredStrikeCritBuffGuid)
                .SetDisplayName(InspiredStrikeCritAbilityDisplayName)
                .SetDescription(InspiredStrikeCritAbilityDescription)
                .SetIcon(ActivatableAbilityRefs.ArcaneWeaponKeenChoice.Reference.Get().Icon)
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .AddNotDispelable()
                .Configure();

            var inspired_crit_ability = ActivatableAbilityConfigurator.New(InspiredStrikeCritAbility, InspiredStrikeCritAbilityGuid)
                .SetDisplayName(InspiredStrikeCritAbilityDisplayName)
                .SetDescription(InspiredStrikeCritAbilityDescription)
                .SetIcon(ActivatableAbilityRefs.ArcaneWeaponKeenChoice.Reference.Get().Icon)
                .SetBuff(inspired_crit_buff)
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ResourceSpendType.Never)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(InspiredStrike, InspiredStrikeGuid)
                .SetDisplayName(InspiredStrikeDisplayName)
                .SetDescription(InspiredStrikeDescription)
                .SetIcon(FeatureRefs.SacredWeaponEnchantFeature.Reference.Get().Icon)
                .AddFacts(new() { inspired_ability, inspired_crit_ability })
                .SetIsClassFeature()
                .Configure();
        }

        private static BlueprintFeature CreateDeeds11()
        {
            return FeatureConfigurator.New(InspiredDeeds11, InspiredDeeds11Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateInspiredStrike(), Swashbuckler.evasive_feature, Swashbuckler.subtle_feature })
                .SetIsClassFeature()
                .Configure();
        }

    }
}
