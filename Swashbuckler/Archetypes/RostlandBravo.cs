using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Swashbuckler.Components;
using Swashbuckler.Feats;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;

namespace Swashbuckler.Archetypes
{
    internal class RostlandBravo
    {
        internal static BlueprintFeature bravo_profs;
        internal static BlueprintFeature bravo_deeds3;
        internal static BlueprintFeature bravo_deeds7;
        internal static BlueprintFeature bravo_deeds11;
        internal static BlueprintFeature bravo_deeds15;

        private const string ArchetypeName = "RostlandBravo";
        private const string ArchetypeGuid = "1D17966F-6EDB-4668-BC6D-EE671C1667AF";
        private const string ArchetypeDisplayName = "RostlandBravo.Name";
        private const string ArchetypeDescription = "RostlandBravo.Description";

        private const string ProfName = "RostlandBravoProf";
        private const string ProfGuid = "B5E4A2AC-4EE1-4561-BFC7-6899C5E488D2";
        private const string ProfDisplayName = "RostlandBravoProf.Name";
        private const string ProfDescription = "RostlandBravoProf.Description";

        private const string InevitableName = "RostlandBravoInevitable";
        private const string InevitableGuid = "E5C2CB56-53B0-4467-AFE0-C2943ADE50F9";
        private const string InevitableAbilityName = "RostlandBravoInevitableAbility";
        private const string InevitableAbilityGuid = "F1716143-C68E-46CA-99CE-18FF3F305D72";
        private const string InevitableDisplayName = "RostlandBravoInevitable.Name";
        private const string InevitableDescription = "RostlandBravoInevitable.Description";

        private const string SweepingName = "RostlandBravoSweeping";
        private const string SweepingGuid = "8EFFEAD1-C7E2-4F5C-88E7-C86EC5BD751E";
        private const string SweepingAbilityName = "RostlandBravoSweepingAbility";
        private const string SweepingAbilityGuid = "05604DA6-B2F0-45D8-BF7B-EA8DA0D2459F";
        private const string SweepingDisplayName = "RostlandBravoSweeping.Name";
        private const string SweepingDescription = "RostlandBravoSweeping.Description";

        private const string RageName = "RostlandBravoRage";
        private const string RageGuid = "9BC26F16-0E92-4B1C-8003-9331F89DBB0A";
        private const string RageAbilityName = "RostlandBravoRageAbility";
        private const string RageAbilityGuid = "307B74AF-4D67-43E4-B134-4F480C87F1A5";
        private const string RageBuffName = "RostlandBravoRageBuff";
        private const string RageBuffGuid = "AA07C4C2-CEF0-4D66-BDD3-EBD04AB57EF2";
        private const string RageDisplayName = "RostlandBravoRage.Name";
        private const string RageDescription = "RostlandBravoRage.Description";

        private const string TerrorName = "RostlandBravoTerror";
        private const string TerrorGuid = "2E416959-F9E2-4182-8B3F-BCE885104900";
        private const string TerrorAbilityName = "RostlandBravoTerrorAbility";
        private const string TerrorAbilityGuid = "CA7CA49A-A42F-4822-83C1-7FF273C5C6CE";
        private const string TerrorBuffName = "RostlandBravoTerrorBuff";
        private const string TerrorBuffGuid = "62A8BC89-F628-476C-9D49-EC402AEB4EDF";
        private const string TerrorDisplayName = "RostlandBravoTerror.Name";
        private const string TerrorDescription = "RostlandBravoTerror.Description";

        private const string Deeds3Name = "RostlandBravoDeeds3";
        private const string Deeds3Guid = "A2664765-7235-47A6-A276-072CBE95DBF8";
        private const string Deeds7Name = "RostlandBravoDeeds7";
        private const string Deeds7Guid = "4BFB290E-9749-4D70-8FF6-0F64751B6ED2";
        private const string Deeds11Name = "RostlandBravoDeeds11";
        private const string Deeds11Guid = "9CE41BE9-4726-4735-A772-F66F707221CC";
        private const string Deeds15Name = "RostlandBravoDeeds15";
        private const string Deeds15Guid = "0E0EA15B-3011-4DCE-80B7-F8D1E4A4346E";

        internal static void Configure()
        {
            var archetype =
              ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, Swashbuckler.swash_class)
                .SetLocalizedName(ArchetypeDisplayName)
                .SetLocalizedDescription(ArchetypeDescription)
                .SetClassSkills(StatType.SkillMobility, StatType.SkillPersuasion, StatType.SkillKnowledgeWorld, StatType.SkillPerception, StatType.SkillThievery)
                .SetReplaceClassSkills()
                .SetStartingItems(ItemWeaponRefs.ColdIronDuelingSword.Reference.Get(), ItemArmorRefs.LeatherStandard.Reference.Get(), ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get())
                .SetReplaceStartingEquipment();

            archetype
              .AddToRemoveFeatures(1, Swashbuckler.profs)
              .AddToRemoveFeatures(3, Swashbuckler.deeds3)
              .AddToRemoveFeatures(7, Swashbuckler.deeds7)
              .AddToRemoveFeatures(11, Swashbuckler.deeds11)
              .AddToRemoveFeatures(15, Swashbuckler.deeds15);

            bravo_profs = CreateProfs();
            bravo_deeds3 = CreateDeeds3();
            bravo_deeds7 = CreateDeeds7();
            bravo_deeds11 = CreateDeeds11();
            bravo_deeds15 = CreateDeeds15();

            archetype
                .AddToAddFeatures(1, bravo_profs, FeatureRefs.DuelingMastery.Reference.Get())
                .AddToAddFeatures(3, bravo_deeds3)
                .AddToAddFeatures(7, bravo_deeds7)
                .AddToAddFeatures(11, bravo_deeds11)
                .AddToAddFeatures(15, bravo_deeds15);


            archetype.Configure();
        }

        internal static BlueprintFeature CreateProfs()
        {
            return FeatureConfigurator.New(ProfName, ProfGuid)
              .SetDisplayName(ProfDisplayName)
              .SetDescription(ProfDescription)
              .SetIcon(FeatureRefs.DuelingSwordProficiency.Reference.Get().Icon)
              .AddFacts(new()
              {
                  FeatureRefs.SimpleWeaponProficiency.Reference.Get(),
                  FeatureRefs.MartialWeaponProficiency.Reference.Get(),
                  FeatureRefs.LightArmorProficiency.Reference.Get(),
                  FeatureRefs.DuelingSwordProficiency.Reference.Get()
              })
              .SetIsClassFeature()
              .Configure();
        }

        internal static BlueprintFeature CreateInevitable()
        {
            var ab = AbilityConfigurator.New(InevitableAbilityName, InevitableAbilityGuid)
                .SetDisplayName(InevitableDisplayName)
                .SetDescription(InevitableDescription)
                .SetIcon(AbilityRefs.StunningBarrier.Reference.Get().Icon)
                .SetActionType(UnitCommand.CommandType.Standard)
                .AddAbilityEffectRunAction(ActionsBuilder.New().CastSpell(AbilityRefs.DazzlingDisplayAction.Reference.Get()).Build())
                .AddComponent<AbilityCasterDuelingSwordCheck>()
                .AddAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, amount: 1, isSpendResource: true)
                .AddComponent<AttackAnimation>()
                .Configure();

            var feat = FeatureConfigurator.New(InevitableName, InevitableGuid)
                .SetDisplayName(InevitableDisplayName)
                .SetDescription(InevitableDescription)
                .SetIcon(AbilityRefs.StunningBarrier.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddFacts(new() { ab })
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureRefs.DazzlingDisplayFeature.Reference.deserializedGuid } })
                .Configure();

            FeatureConfigurator.For(FeatureRefs.DazzlingDisplayFeature.Reference.Get())
                .AddRecommendationNoFeatFromGroup(new() { feat })
                .Configure();

            return feat;
        }

        internal static BlueprintFeature CreateDeeds3()
        {
            return FeatureConfigurator.New(Deeds3Name, Deeds3Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateInevitable(), Swashbuckler.swash_init_feature, Swashbuckler.kip_feature, Swashbuckler.precise_feat })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateSweeping()
        {
            var ab = AbilityConfigurator.New(SweepingAbilityName, SweepingAbilityGuid)
                .SetDisplayName(SweepingDisplayName)
                .SetDescription(SweepingDescription)
                .SetIcon(AbilityRefs.CauseFear.Reference.Get().Icon)
                .AllowTargeting(enemies: true)
                .SetActionType(UnitCommand.CommandType.Swift)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityCasterDuelingSwordCheck>()
                .AddAbilityEffectRunAction(FeintFeats.vanilla_feint_action)
                .AddAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, amount: 1, isSpendResource: true)
                .Configure();

            return FeatureConfigurator.New(SweepingName, SweepingGuid)
                .SetDisplayName(SweepingDisplayName)
                .SetDescription(SweepingDescription)
                .SetIcon(AbilityRefs.CauseFear.Reference.Get().Icon)
                .SetIsClassFeature()
                .AddFacts(new() { ab })
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds7()
        {
            return FeatureConfigurator.New(Deeds7Name, Deeds7Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.targeted_strike_feat, Swashbuckler.grace_feat, CreateSweeping() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateRage()
        {
            var buff = BuffConfigurator.New(RageBuffName, RageBuffGuid)
                .SetDisplayName(RageDisplayName)
                .SetDescription(RageDescription)
                .SetIcon(AbilityRefs.Scare.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<DragonsRage>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var ab = ActivatableAbilityConfigurator.New(RageAbilityName, RageAbilityGuid)
                .SetDisplayName(RageDisplayName)
                .SetDescription(RageDescription)
                .SetIcon(AbilityRefs.Scare.Reference.Get().Icon)
                .SetBuff(buff)
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ResourceSpendType.Never)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(RageName, RageGuid)
                .SetDisplayName(RageDisplayName)
                .SetDescription(RageDescription)
                .SetIcon(AbilityRefs.Scare.Reference.Get().Icon)
                .AddFacts(new() { ab })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds11()
        {
            return FeatureConfigurator.New(Deeds11Name, Deeds11Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.subtle_feature, Swashbuckler.evasive_feature, CreateRage() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateTerror()
        {
            var buff = BuffConfigurator.New(TerrorBuffName, TerrorBuffGuid)
                .SetDisplayName(TerrorDisplayName)
                .SetDescription(TerrorDescription)
                .SetIcon(AbilityRefs.Fear.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<WyrmTerror>()
                .Configure();

            var ab = ActivatableAbilityConfigurator.New(TerrorAbilityName, TerrorAbilityGuid)
                .SetDisplayName(TerrorDisplayName)
                .SetDescription(TerrorDescription)
                .SetIcon(AbilityRefs.Fear.Reference.Get().Icon)
                .SetBuff(buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(TerrorName, TerrorGuid)
                .SetDisplayName(TerrorDisplayName)
                .SetDescription(TerrorDescription)
                .SetIcon(AbilityRefs.Fear.Reference.Get().Icon)
                .AddFacts(new() { ab })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds15()
        {
            return FeatureConfigurator.New(Deeds15Name, Deeds15Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.dizzying_feat, Swashbuckler.perfect_feat, CreateTerror() })
                .SetIsClassFeature()
                .Configure();
        }
    }
}
