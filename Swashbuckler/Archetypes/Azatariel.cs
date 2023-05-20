using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Swashbuckler.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.RuleSystem.Rules.RuleDispelMagic;
using static Kingmaker.UnitLogic.FactLogic.AddMechanicsFeature;
using static Kingmaker.UnitLogic.Mechanics.Actions.ContextActionDispelMagic;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace Swashbuckler.Archetypes
{
    internal class Azatariel
    {
        static internal BlueprintArchetype azatariel;

        static internal BlueprintFeature azata_deeds3;
        static internal BlueprintFeature azata_deeds7;
        static internal BlueprintFeature azata_deeds11;

        static internal BlueprintFeature elysian_buff;
        static internal BlueprintFeature elysian_feat;

        static internal BlueprintFeature battle_dance;

        static internal BlueprintFeature affection;

        static internal BlueprintFeature whimsical_feat;
        static internal BlueprintBuff whimsical_buff;
        static internal BlueprintFeature whimsical_aoo_buff;

        static internal BlueprintFeature lillend_feat;
        static internal BlueprintBuff lillend_buff;

        private const string ArchetypeName = "Azatariel";
        private const string ArchetypeGuid = "BEC40A4A-6643-4C45-8237-42DBFC644F70";
        private const string ArchetypeDisplayName = "Azatariel.Name";
        private const string ArchetypeDescription = "Azatariel.Description";

        private const string BralaniName = "Bralani";
        private const string BralaniGuid = "B7FECF9A-86EE-4346-9EAB-7403B4727EF8";
        private const string BralaniDisplayName = "Bralani.Name";
        private const string BralaniDescription = "Bralani.Description";

        private const string ElysianName = "ElysianConviction";
        private const string ElysianGuid = "40C497B1-8011-4702-AD9E-756ACA6AB7D4";
        private const string ElysianBuffName = "ElysianConvictionBuff";
        private const string ElysianBuffGuid = "C155C664-7F65-4C75-8346-BB7497D04DBD";
        private const string ElysianDisplayName = "ElysianConviction.Name";
        private const string ElysianDescription = "ElysianConviction.Description";

        private const string WhimsicalName = "Whimsical";
        private const string WhimsicalGuid = "7452617C-966D-4CBA-A0B7-5F98D0248D83";
        private const string WhimsicalBuffName = "WhimsicalBuff";
        private const string WhimsicalBuffGuid = "3385370D-6DB9-42A6-847B-A2B56926B75F";
        private const string WhimsicalAoOBuffName = "WhimsicalAoOBuff";
        private const string WhimsicalAoOBuffGuid = "830DE505-5900-4F92-811F-2E9A50C43048";
        private const string WhimsicalAbilityName = "WhimsicalAbility";
        private const string WhimsicalAbilityGuid = "0DA8E9EE-F80D-4424-95B5-1EA8E1FFD61F";
        private const string WhimsicalDisplayName = "Whimsical.Name";
        private const string WhimsicalDescription = "Whimsical.Description";

        private const string LillendName = "Lillend";
        private const string LillendGuid = "478BD0C9-F13F-4A61-9FD0-21A4EED49BE7";
        private const string LillendBuffName = "LillendBuff";
        private const string LillendBuffGuid = "DAB10F59-7A3F-4E41-B56F-7E8157A967B3";
        private const string LillendAbilityName = "LillendAbility";
        private const string LillendAbilityGuid = "C6ED2365-4BA7-4175-B76E-3D3A9D28E939";
        private const string LillendDisplayName = "Lillend.Name";
        private const string LillendDescription = "Lillend.Description";

        private const string BattleDance = "BattleDance";
        private const string BattleDanceGuid = "509467CE-CFC9-4154-8EF7-1DC7F90234CE";
        private const string BattleDanceBuff = "BattleDanceBuff";
        private const string BattleDanceBuffGuid = "E4312859-5E3B-45FA-AE47-CB53924415B2";
        private const string BattleDanceDisplayName = "BattleDance.Name";
        private const string BattleDanceDescription = "BattleDance.Description";

        private const string Affection = "Affection";
        private const string AffectionGuid = "A51C6F87-F7ED-4DF7-9092-0DAC7EF31F9F";
        private const string AffectionResource = "AffectionResource";
        private const string AffectionResourceGuid = "1815D0B8-BABD-442E-B95A-2D4CF2CEE2CB";
        private const string AffectionAbility = "AffectionAbility";
        private const string AffectionAbilityGuid = "CED8015A-494A-492B-998F-0ABC78187D87";
        private const string AffectionDisplayName = "Affection.Name";
        private const string AffectionDescription = "Affection.Description";

        private const string GhaeleAbility = "GhaeleAbility";
        private const string GhaeleAbilityGuid = "24D49FFD-0883-4F96-9E03-3B58A853E862";
        private const string Ghaele = "Ghaele";
        private const string GhaeleGuid = "43A40A65-342B-4464-93FD-11286BBB6F54";
        private const string GhaeleBuff = "GhaeleBuff";
        private const string GhaeleBuffGuid = "E7A3CBE9-B57F-4D23-913F-0FC849053821";
        private const string GhaeleDebuff = "GhaeleDebuff";
        private const string GhaeleDebuffGuid = "9A1942E6-D436-480E-833B-640D55647C09";
        private const string GhaeleDisplayName = "Ghaele.Name";
        private const string GhaeleDescription = "Ghaele.Description";

        private const string AzataDeeds3 = "AzataDeeds3";
        private const string AzataDeeds3Guid = "464E9325-7C9A-47B6-9641-B2DDA58F1C39";

        private const string AzataDeeds7 = "AzataDeeds7";
        private const string AzataDeeds7Guid = "42DD4B90-72D7-4E7D-95C6-66C4965B6BBB";

        private const string AzataDeeds11 = "AzataDeeds11";
        private const string AzataDeeds11Guid = "AD3B3691-3DC2-4FF9-AC96-42760E8EA9A9";

        internal static void Configure()
        {
            var archetype =
              ArchetypeConfigurator.New(ArchetypeName, ArchetypeGuid, Swashbuckler.swash_class)
                .SetLocalizedName(ArchetypeDisplayName)
                .SetLocalizedDescription(ArchetypeDescription)
                .AddPrerequisiteAlignment(AlignmentMaskType.ChaoticGood);

            archetype
              .AddToRemoveFeatures(2, Swashbuckler.charmed_life)
              .AddToRemoveFeatures(3, Swashbuckler.nimble, Swashbuckler.deeds3)
              .AddToRemoveFeatures(4, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(7, Swashbuckler.deeds7)
              .AddToRemoveFeatures(8, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(11, Swashbuckler.deeds11)
              .AddToRemoveFeatures(12, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(16, Swashbuckler.swash_bonus_feats)
              .AddToRemoveFeatures(20, Swashbuckler.swash_bonus_feats);

            elysian_feat = CreateElysian();
            battle_dance = CreateDance();
            affection = CreateAffection();

            azata_deeds3 = CreateDeeds3();
            azata_deeds7 = CreateDeeds7();
            azata_deeds11 = CreateDeeds11();

            archetype
                .AddToAddFeatures(2, elysian_feat)
                .AddToAddFeatures(3, battle_dance, azata_deeds3)
                .AddToAddFeatures(4, affection, FeatureSelectionRefs.SelectionMercy.Reference.Get())
                .AddToAddFeatures(7, azata_deeds7)
                .AddToAddFeatures(8, FeatureSelectionRefs.SelectionMercy.Reference.Get())
                .AddToAddFeatures(11, azata_deeds11)
                .AddToAddFeatures(12, FeatureSelectionRefs.SelectionMercy.Reference.Get())
                .AddToAddFeatures(16, FeatureSelectionRefs.SelectionMercy.Reference.Get())
                .AddToAddFeatures(20, FeatureSelectionRefs.SelectionMercy.Reference.Get());

            azatariel = archetype.Configure();
        }

        internal static BlueprintFeature CreateBralani()
        {
            return FeatureConfigurator.New(BralaniName, BralaniGuid)
                .SetDisplayName(BralaniDisplayName)
                .SetDescription(BralaniDescription)
                .AddComponent<Bralani>()
                .SetIcon(AbilityRefs.Haste.Reference.Get().Icon)
                .Configure();
        }

        internal static BlueprintFeature CreateRiposte()
        {
            whimsical_aoo_buff = FeatureConfigurator.New(WhimsicalAoOBuffName, WhimsicalAoOBuffGuid)
                .SetHideInUI()
                .SetHideInCharacterSheetAndLevelUp()
                .AddAttackOfOpportunityAttackBonus(bonus: ContextValues.Rank())
                .AddAttackOfOpportunityDamageBonus(damageBonus: ContextValues.Rank())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .Configure();

            whimsical_buff = BuffConfigurator.New(WhimsicalBuffName, WhimsicalBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .Configure();

            var whimsical_ability = ActivatableAbilityConfigurator.New(WhimsicalAbilityName, WhimsicalAbilityGuid)
                .SetDisplayName(WhimsicalDisplayName)
                .SetDescription(WhimsicalDescription)
                .SetBuff(whimsical_buff)
                .SetDeactivateImmediately()
                .SetIcon(AbilityRefs.HideousLaughter.Reference.Get().Icon)
                .Configure();

            whimsical_feat = FeatureConfigurator.New(WhimsicalName, WhimsicalGuid)
                .SetDisplayName(WhimsicalDisplayName)
                .SetDescription(WhimsicalDescription)
                .AddFacts(new() { whimsical_ability })
                .SetIcon(AbilityRefs.HideousLaughter.Reference.Get().Icon)
                .Configure();

            return whimsical_feat;
        }

        private static BlueprintFeature CreateDeeds3()
        {
            return FeatureConfigurator.New(AzataDeeds3, AzataDeeds3Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateBralani(), CreateRiposte(), Swashbuckler.swash_init_feature, Swashbuckler.kip_feature })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateLillend()
        {
            lillend_buff = BuffConfigurator.New(LillendBuffName, LillendBuffGuid)
                .SetDisplayName(LillendDisplayName)
                .SetDescription(LillendDescription)
                .AddNotDispelable()
                .AddComponent<Lillend>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .SetIcon(AbilityRefs.Blink.Reference.Get().Icon)
                .Configure();

            var lillend_ability = ActivatableAbilityConfigurator.New(LillendAbilityName, LillendAbilityGuid)
                .SetDisplayName(LillendDisplayName)
                .SetDescription(LillendDescription)
                .SetBuff(lillend_buff)
                .SetDeactivateImmediately()
                .AddActivatableAbilityResourceLogic(requiredResource: Swashbuckler.panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetIcon(AbilityRefs.Blink.Reference.Get().Icon)
                .Configure();

            lillend_feat = FeatureConfigurator.New(LillendName, LillendGuid)
                .SetDisplayName(LillendDisplayName)
                .SetDescription(LillendDescription)
                .AddFacts(new() { lillend_ability })
                .SetIcon(AbilityRefs.Blink.Reference.Get().Icon)
                .Configure();

            return lillend_feat;
        }

        private static BlueprintFeature CreateDeeds7()
        {
            return FeatureConfigurator.New(AzataDeeds7, AzataDeeds7Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { Swashbuckler.grace_feat, Swashbuckler.sup_feint_feat, CreateLillend() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateGhaele()
        {
            var ghaele_debuff = BuffConfigurator.New(GhaeleDebuff, GhaeleDebuffGuid)
                .SetDisplayName(GhaeleDisplayName)
                .SetDescription(GhaeleDescription)
                .AddStatBonus(stat: StatType.AC, value: -2)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            var ghaele_buff = BuffConfigurator.New(GhaeleBuff, GhaeleBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddMechanicsFeature(MechanicsFeatureType.Pounce)
                .AddAbilityUseTrigger(ability: AbilityRefs.ChargeAbility.Reference.Get(), action: ActionsBuilder.New().ContextSpendResource(Swashbuckler.panache_resource, ContextValues.Constant(2)).ApplyBuff(buff: ghaele_debuff, durationValue: ContextDuration.Fixed(1), toCaster: true).Build())
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .Configure();

            var ghaele_ability = ActivatableAbilityConfigurator.New(GhaeleAbility, GhaeleAbilityGuid)
                .SetDisplayName(GhaeleDisplayName)
                .SetDescription(GhaeleDescription)
                .SetBuff(ghaele_buff)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(Ghaele, GhaeleGuid)
                .SetDisplayName(GhaeleDisplayName)
                .SetDescription(GhaeleDescription)
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon)
                .AddFacts(new() { ghaele_ability })
                .Configure();
        }

        private static BlueprintFeature CreateDeeds11()
        {
            return FeatureConfigurator.New(AzataDeeds11, AzataDeeds11Guid)
                .SetDisplayName(Swashbuckler.DeedsDisplayName)
                .SetDescription(Swashbuckler.DeedsDescription)
                .AddFacts(new() { CreateGhaele(), Swashbuckler.evasive_feature, Swashbuckler.subtle_feature })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateElysian()
        {
            elysian_buff = FeatureConfigurator.New(ElysianBuffName, ElysianBuffGuid)
                .SetHideInUI(true)
                .SetHideInCharacterSheetAndLevelUp(true)
                .AddSavingThrowBonusAgainstDescriptor(spellDescriptor: SpellDescriptor.MindAffecting, bonus: ContextValues.Rank())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma))
                .Configure();

            return FeatureConfigurator.New(ElysianName, ElysianGuid)
                .SetDisplayName(ElysianDisplayName)
                .SetDescription(ElysianDescription)
                .SetIcon(FeatureRefs.WitchHexFortuneFeature.Reference.Get().Icon)
                .AddComponent<ElysianConviction>()
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDance()
        {
            var dance_buff = BuffConfigurator.New(BattleDanceBuff, BattleDanceBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(classes: new string[] { Swashbuckler.SwashName }).WithStartPlusDivStepProgression(start: 3, divisor: 4, delayStart: true))
                .AddContextStatBonus(stat: StatType.Speed, descriptor: ModifierDescriptor.Enhancement, value: ContextValues.Rank(),multiplier: 10)
                .Configure();

            return FeatureConfigurator.New(BattleDance, BattleDanceGuid)
                .SetDisplayName(BattleDanceDisplayName)
                .SetDescription(BattleDanceDescription)
                .AddBuffOnLightOrNoArmor(dance_buff)
                .SetIcon(FeatureRefs.SwiftFootFeature.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateAffection()
        {
            var aff_resource = AbilityResourceConfigurator.New(AffectionResource, AffectionResourceGuid)
                .SetMaxAmount(ResourceAmountBuilder.New(0).IncreaseByStat(StatType.Charisma).IncreaseByLevelStartPlusDivStep(new string[] { Swashbuckler.SwashName }, 0, 0, 0, 2, 1, 1))
                .Configure();

            var aff_ability = AbilityConfigurator.New(AffectionAbility, AffectionAbilityGuid)
                .SetDisplayName(AffectionDisplayName)
                .SetDescription(AffectionDescription)
                .SetType(AbilityType.Supernatural)
                .AllowTargeting(friends: true)
                .SetRange(AbilityRange.Touch)
                .SetAnimation(CastAnimationStyle.Touch)
                .AddAbilitySpawnFx(prefabLink: AbilityRefs.LayOnHandsOthers.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink)
                .AddAbilityResourceLogic(requiredResource: aff_resource, amount: 1, isSpendResource: true)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyFatigued.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 524288 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyDazed.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 65536 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercySickened.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 131072 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyShaken.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 262144 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyStaggered.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 1048576 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyNauseated.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 2097152 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyFrightened.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 4194304 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyExhausted.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 8388608 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyStunned.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 16777216 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyParalyzed.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 33554432 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyConfused.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 67108864 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyBlinded.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 134217728 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyDiseased.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 16384 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyPoisoned.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 256 }))
                    .Conditional(ConditionsBuilder.New().CasterHasFact(FeatureRefs.MercyCursed.Reference.Get()), ifTrue: ActionsBuilder.New().DispelMagic(buffType: BuffType.All, checkType: CheckType.None, maxSpellLevel: ContextValues.Constant(0), descriptor: new SpellDescriptorWrapper() { m_IntValue = 268435456 }))
                    )
                .SetIcon(AbilityRefs.MageLight.Reference.Get().Icon)
                .Configure();

            return FeatureConfigurator.New(Affection, AffectionGuid)
                .SetDisplayName(AffectionDisplayName)
                .SetDescription(AffectionDescription)
                .AddFacts(new() { aff_resource, aff_ability })
                .SetIcon(AbilityRefs.MageLight.Reference.Get().Icon)
                .SetIsClassFeature()
                .Configure();
        }
    }
}
