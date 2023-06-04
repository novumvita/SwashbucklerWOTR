using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Swashbuckler.Components;

namespace Swashbuckler.Archetypes
{
    internal class WarriorPoet
    {
        static internal BlueprintArchetype warrior_poet;

        static internal BlueprintFeature dancers_grace;

        static internal BlueprintFeature dancer_deeds1;
        static internal BlueprintFeature dancer_deeds3;
        static internal BlueprintFeature dancer_deeds7;
        static internal BlueprintFeature dancer_deeds11;
        static internal BlueprintFeature dancer_deeds15;

        internal const string ArchetypeName = "WarriorPoet";
        internal const string ArchetypeGuid = "2CDE6BB0-3EB9-4A93-B562-DF74DDD4EE1E";
        internal const string ArchetypeDisplayName = "WarriorPoet.Name";
        internal const string ArchetypeDescription = "WarriorPoet.Description";

        internal const string FinesseName = "DancersFinesse";
        internal const string FinesseGuid = "9A0276F4-060B-4057-A709-6F6FA004F58C";
        internal const string FinesseDisplayName = "DancersFinesse.Name";
        internal const string FinesseDescription = "DancersFinesse.Description";

        internal const string StrikeName = "DancersStrike";
        internal const string StrikeGuid = "7D21870C-5D7A-496C-90E9-BBCF6FCCE202";
        internal const string StrikeDisplayName = "DancersStrike.Name";
        internal const string StrikeDescription = "DancersStrike.Description";

        internal const string GraceName = "DancersGrace";
        internal const string GraceGuid = "7865266C-AD52-4E8C-9180-2752C4334C51";
        internal const string GraceDisplayName = "DancersGrace.Name";
        internal const string GraceDescription = "DancersGrace.Description";

        internal const string BloomName = "DancersBloom";
        internal const string BloomGuid = "3AF1E76F-8EE9-4AEE-80D0-7C72127CC2BD";
        internal const string BloomDisplayName = "DancersBloom.Name";
        internal const string BloomDescription = "DancersBloom.Description";

        internal const string HarmonyName = "DancersHarmony";
        internal const string HarmonyGuid = "3BCBB741-5A56-4D1C-88CD-FD30CCB7923C";
        internal const string HarmonyDisplayName = "DancersHarmony.Name";
        internal const string HarmonyDescription = "DancersHarmony.Description";

        internal const string BattleDance = "DancersBattleDance";
        internal const string BattleDanceGuid = "AFE4E58F-608A-490F-B64C-4A44CCD49C8B";
        internal const string BattleDanceBuff = "DancersBattleDanceBuff";
        internal const string BattleDanceBuffGuid = "6E29CC4D-5D24-40EA-9D65-B93E34AE1D9E";
        internal const string BattleDanceDisplayName = "DancersBattleDance.Name";
        internal const string BattleDanceDescription = "DancersBattleDance.Description";

        internal const string DancerDeeds1 = "DancerDeeds1";
        internal const string DancerDeeds1Guid = "4CE5A8B6-1706-44D9-A083-8C5B8A487E09";
        internal const string DancerDeeds3 = "DancerDeeds3";
        internal const string DancerDeeds3Guid = "987A4A35-D3B5-4CF4-92A1-84D02E09E826";
        internal const string DancerDeeds7 = "DancerDeeds7";
        internal const string DancerDeeds7Guid = "C3A50FBC-3F86-4E05-8483-F02667B4BA4F";
        internal const string DancerDeeds11 = "DancerDeeds11";
        internal const string DancerDeeds11Guid = "C2899704-511E-4702-8C89-B45D8E8B7B11";
        internal const string DancerDeeds15 = "DancerDeeds15";
        internal const string DancerDeeds15Guid = "E91A9139-4803-437F-A05B-5DC3DB5B161A";

        internal const string ParryBuff = "DancerParryBuff";
        internal const string ParryBuffGuid = "373423D1-C46A-468E-8FAC-1897C71483BB";
        internal const string ParryAbility = "DancerParryAbility";
        internal const string ParryAbilityGuid = "4D48823F-B532-49CE-AC7B-95BC7828568E";
        internal const string ParryFeature = "DancerParryFeature";
        internal const string ParryFeatureGuid = "72199229-4387-41A8-8358-C1072BCE12C3";
        internal const string ParryDisplayName = "DancersParry.Name";
        internal const string ParryDescription = "DancersParry.Description";

        internal const string EdgeFeature = "DancersEdge";
        internal const string EdgeFeatureGuid = "CBF72B60-5095-4669-AEDF-E2B5C41F91AF";
        internal const string EdgeAbility = "DancersEdgeAbility";
        internal const string EdgeAbilityGuid = "194343D6-7205-4500-AA9E-576BB414EAEF";
        internal const string EdgeBuff = "DancersEdgeBuff";
        internal const string EdgeBuffGuid = "48101A8C-6DF9-47E7-ADA4-CE61FF374A6F";
        internal const string EdgeDisplayName = "DancersEdge.Name";
        internal const string EdgeDescription = "DancersEdge.Description";

        internal const string KitsuneName = "DancersKitsune";
        internal const string KitsuneGuid = "9E69BA56-0139-46FD-A3DF-5985E38FC44A";
        internal const string KitsuneDisplayName = "DancersKitsune.Name";
        internal const string KitsuneDescription = "DancersKitsune.Description";
        internal const string KitsuneAbilityName = "DancersKitsuneAbility";
        internal const string KitsuneAbilityGuid = "D603F8CB-6A20-45EE-BA72-0043052E1A39";

        internal const string HastedName = "DancersHasted";
        internal const string HastedGuid = "0266048B-B9E8-4B99-A7C8-9753A64A4B36";
        internal const string HastedDisplayName = "DancersHasted.Name";
        internal const string HastedDescription = "DancersHasted.Description";
        internal const string HastedAbilityName = "DancersHastedAbility";
        internal const string HastedAbilityGuid = "AE6B1A20-FA93-4E9C-91BA-3DA4CBA2A347";
        internal const string HastedBuffName = "DancersHastedBuff";
        internal const string HastedBuffGuid = "798302A7-6DB1-48C4-A6DA-074B277D0451";

        internal static void Configure()
        {
            var archetype =
              ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, Swashbuckler.swash_class)
                .SetLocalizedName(ArchetypeDisplayName)
                .SetLocalizedDescription(ArchetypeDescription);

            archetype
              .AddToRemoveFeatures(1, Swashbuckler.swash_finesse, Swashbuckler.deeds1)
              .AddToRemoveFeatures(2, Swashbuckler.charmed_life)
              .AddToRemoveFeatures(3, Swashbuckler.nimble, Swashbuckler.deeds3)
              .AddToRemoveFeatures(4, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(7, Swashbuckler.deeds7)
              .AddToRemoveFeatures(8, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(11, Swashbuckler.deeds11)
              .AddToRemoveFeatures(12, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(15, Swashbuckler.deeds15)
              .AddToRemoveFeatures(16, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(20, Swashbuckler.swash_bonus_feats);
            dancers_grace = CreateGrace();

            dancer_deeds1 = CreateDeeds1();
            dancer_deeds3 = CreateDeeds3();
            dancer_deeds7 = CreateDeeds7();
            dancer_deeds11 = CreateDeeds11();
            dancer_deeds15 = CreateDeeds15();

            archetype
                .AddToAddFeatures(1, Swashbuckler.dancers_finesse, dancer_deeds1)
                .AddToAddFeatures(2, dancers_grace)
                .AddToAddFeatures(3, dancer_deeds3, CreateDance())
                .AddToAddFeatures(4, Feats.SpringAttack.springAttackFeat)
                .AddToAddFeatures(7, dancer_deeds7)
                .AddToAddFeatures(8, Feats.FeintFeats.feint_feat)
                .AddToAddFeatures(11, dancer_deeds11)
                .AddToAddFeatures(12, Feats.SpringAttack.springAttack1)
                .AddToAddFeatures(15, dancer_deeds15)
                .AddToAddFeatures(16, Feats.SpringAttack.springAttack2)
                .AddToAddFeatures(20, Feats.FeintFeats.greater_feint_feat);

            warrior_poet = archetype.Configure();
        }

        internal static BlueprintFeature CreateFinesse()
        {
            var swash_finesse = FeatureConfigurator.New(FinesseName, FinesseGuid)
                .SetDisplayName(FinesseDisplayName)
                .SetDescription(FinesseDescription)
                .SetIcon(FeatureRefs.WeaponFinesse.Reference.Get().Icon)
                .AddComponent<AttackStatReplacementForSwashbucklerWeapon>()
                .AddReplaceStatForPrerequisites(StatType.Charisma, StatType.Intelligence)
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureRefs.WeaponFinesse.Reference.deserializedGuid } })
                .SetIsClassFeature()
                .AddComponent<AddDuelistWeapon>(c => c.WeaponCategory = WeaponCategory.Glaive)
                .Configure();

            FeatureConfigurator.For(FeatureRefs.WeaponFinesse)
                .AddRecommendationNoFeatFromGroup(new() { swash_finesse })
                .Configure();

            return swash_finesse;
        }

        internal static BlueprintFeature CreateGrace()
        {
            var dancers_grace = FeatureConfigurator.New(GraceName, GraceGuid)
                .SetDisplayName(GraceDisplayName)
                .SetDescription(GraceDescription)
                .AddComponent<DancersGrace>()
                .SetIcon(FeatureRefs.WitchHexFortuneFeature.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();

            return dancers_grace;
        }

        internal static BlueprintFeature CreateKitsune()
        {
            var ab = ActivatableAbilityConfigurator.New(KitsuneAbilityName, KitsuneAbilityGuid)
                .SetDisplayName(KitsuneDisplayName)
                .SetDescription(KitsuneDescription)
                .SetBuff(Feats.SpringAttack.kitsune)
                .SetDeactivateImmediately()
                .SetIcon(AbilityRefs.FoxsCunning.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .Configure();

            return FeatureConfigurator.New(KitsuneName, KitsuneGuid)
                .SetDisplayName(KitsuneDisplayName)
                .SetDescription(KitsuneDescription)
                .AddFacts(new() { ab })
                .SetIcon(AbilityRefs.FoxsCunning.Reference.Get().Icon)
                .Configure();
        }

        internal static BlueprintFeature CreateDance()
        {
            var dance_buff = BuffConfigurator.New(BattleDanceBuff, BattleDanceBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(classes: new string[] { Swashbuckler.SwashName }).WithStartPlusDivStepProgression(start: 3, divisor: 4, delayStart: true))
                .AddContextStatBonus(stat: StatType.Speed, descriptor: ModifierDescriptor.Enhancement, value: ContextValues.Rank(), multiplier: 10)
                .Configure();

            return FeatureConfigurator.New(BattleDance, BattleDanceGuid)
                .SetDisplayName(BattleDanceDisplayName)
                .SetDescription(BattleDanceDescription)
                .AddBuffOnLightOrNoArmor(dance_buff)
                .SetIcon(FeatureRefs.SwiftFootFeature.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateStrike()
        {
            return FeatureConfigurator.New(StrikeName, StrikeGuid)
                .SetDisplayName(StrikeDisplayName)
                .SetDescription(StrikeDescription)
                .SetIcon(FeatureRefs.MasterStrike.Reference.Get().Icon)
                .AddComponent<WarriorPoetStrike>()
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds3()
        {
            return FeatureConfigurator.New(DancerDeeds3, DancerDeeds3Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateStrike(), Swashbuckler.swash_init_feature, Swashbuckler.kip_feature })
                .SetIsClassFeature()
                .Configure();
        }



        internal static BlueprintFeature CreateParry()
        {
            var parry_buff = BuffConfigurator.New(ParryBuff, ParryBuffGuid)
                .SetDisplayName(ParryDisplayName)
                .SetDescription(ParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<ParryAndRiposte>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var parry_ability = ActivatableAbilityConfigurator.New(ParryAbility, ParryAbilityGuid)
                .SetDisplayName(ParryDisplayName)
                .SetDescription(ParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(parry_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(ParryFeature, ParryFeatureGuid)
                .SetDisplayName(ParryDisplayName)
                .SetDescription(ParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddFacts(new() { parry_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds1()
        {
            return FeatureConfigurator.New(DancerDeeds1, DancerDeeds1Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.derring_feat, CreateParry() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateBloom()
        {
            return FeatureConfigurator.New(BloomName, BloomGuid)
                .SetDisplayName(BloomDisplayName)
                .SetDescription(BloomDescription)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .AddFacts(new() { FeatureRefs.VitalStrikeFeature.Reference.Get() })
                .AddComponent<DancersBloom>()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds7()
        {
            return FeatureConfigurator.New(DancerDeeds7, DancerDeeds7Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.grace_feat, Swashbuckler.sup_feint_feat, CreateBloom() })
                .SetIsClassFeature()
                .Configure();
        }



        internal static BlueprintFeature CreateHasted()
        {
            var hasted_buff = BuffConfigurator.New(HastedBuffName, HastedBuffGuid)
                .SetDisplayName(HastedDisplayName)
                .SetDescription(HastedDescription)
                .SetIcon(AbilityRefs.Haste.Reference.Get().Icon)
                .AddCombatStateTrigger(combatEndActions: ActionsBuilder.New().RemoveSelf().Build())
                .AddMovementDistanceTrigger(action: ActionsBuilder.New().IncreaseBuffDuration(ContextDuration.Fixed(1), BuffRefs.HasteBuff.Reference.Get()).Build(), distanceInFeet: 10, limitTiggerCountInOneRound: true, tiggerCountMaximumInOneRound: 1)
                .AddNotDispelable()
                .Configure();

            var hasted_ability = AbilityConfigurator.New(HastedAbilityName, HastedAbilityGuid)
                .SetDisplayName(HastedDisplayName)
                .SetDescription(HastedDescription)
                .SetIcon(AbilityRefs.Haste.Reference.Get().Icon)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .ApplyBuffPermanent(hasted_buff)
                    .ApplyBuff(BuffRefs.HasteBuff.Reference.Get(), ContextDuration.Fixed(1)))
                .AddAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, amount: 2, isSpendResource: true)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Personal)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .SetActionType(UnitCommand.CommandType.Swift)
                .Configure();

            return FeatureConfigurator.New(HastedName, HastedGuid)
                .SetDisplayName(HastedDisplayName)
                .SetDescription(HastedDescription)
                .SetIcon(AbilityRefs.Haste.Reference.Get().Icon)
                .AddFacts(new() { hasted_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds11()
        {
            return FeatureConfigurator.New(DancerDeeds11, DancerDeeds11Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.evasive_feature, CreateKitsune(), CreateHasted() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateEdge()
        {
            var edge_buff = BuffConfigurator.New(EdgeBuff, EdgeBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .AddComponent<WarriorPoetEdge>()
                .Configure();

            var edge_ability = ActivatableAbilityConfigurator.New(EdgeAbility, EdgeAbilityGuid)
                .SetDisplayName(EdgeDisplayName)
                .SetDescription(EdgeDescription)
                .SetIcon(AbilityRefs.Azata1SongOfSeasonsAbility.Reference.Get().Icon)
                .SetBuff(edge_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(EdgeFeature, EdgeFeatureGuid)
                .SetDisplayName(EdgeDisplayName)
                .SetDescription(EdgeDescription)
                .SetIcon(AbilityRefs.Azata1SongOfSeasonsAbility.Reference.Get().Icon)
                .AddFacts(new() { edge_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateHarmony()
        {
            var harmony_feature = FeatureConfigurator.New(HarmonyName, HarmonyGuid)
                .SetDisplayName(HarmonyDisplayName)
                .SetDescription(HarmonyDescription)
                .SetIcon(FeatureRefs.FocusedRageFeature.Reference.Get().Icon)
                .AddComponent<DancerHarmony>()
                .SetIsClassFeature()
                .Configure();

            return harmony_feature;
        }

        internal static BlueprintFeature CreateDeeds15()
        {
            return FeatureConfigurator.New(DancerDeeds15, DancerDeeds15Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateEdge(), CreateHarmony(), Swashbuckler.dizzying_feat })
                .SetIsClassFeature()
                .Configure();
        }
    }
}
