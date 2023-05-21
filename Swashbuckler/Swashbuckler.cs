using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Swashbuckler.Components;
using Swashbuckler.Patches;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace Swashbuckler
{
    internal class Swashbuckler
    {
        static internal BlueprintCharacterClass swash_class;

        static internal BlueprintAbilityResource panache_resource;
        static internal BlueprintFeature panache_feature;

        static internal BlueprintFeature swash_finesse;

        static internal BlueprintFeature charmed_life;

        static internal BlueprintFeature swash_bonus_feats;

        static internal BlueprintFeature nimble;

        static internal BlueprintFeature swash_weapon_training;
        static internal BlueprintFeature swash_weapon_mastery;

        static internal BlueprintFeature deeds3;
        static internal BlueprintFeature deeds7;
        static internal BlueprintFeature deeds11;

        static internal BlueprintBuff kip_buff;
        static internal BlueprintFeature kip_feature;
        
        static internal BlueprintFeature swash_init_feature;

        static internal BlueprintBuff precise_strike_buff;

        static internal BlueprintBuff dizzying_defense_buff;

        static internal BlueprintFeature evasive_feature;
        static internal BlueprintFeature subtle_feature;

        static internal BlueprintFeature grace_feat;
        static internal BlueprintFeature sup_feint_feat;

        internal const string SwashName = "Swashbuckler";
        internal const string SwashGuid = "338ABF27-23C1-4C1A-B0F1-7CD7E3020444";
        internal const string SwashDisplayName = "Swashbuckler.Name";
        internal const string SwashDescription = "Swashbuckler.Description";

        internal const string Proficiencies = "SwashbucklerProficiencies";
        internal const string ProficienciesGuid = "2B04D289-36C1-45FC-9641-EC1D16CEA836";
        internal const string ProficienciesDisplayName = "Proficiencies.Name";
        internal const string ProficienciesDescription = "Proficiencies.Description";

        internal const string Progression = "SwashbucklerProgression";
        internal const string ProgressionGuid = "CB3F7F94-2433-44BE-B20F-4954BC9848C3";

        internal const string PanacheResource = "SwashbucklerPanacheResource";
        internal const string PanacheResourceGuid = "AC63BFCF-EC31-43DC-A5CE-04617A3BC854";
        internal const string PanacheFeature = "SwashbucklerPanacheFeature";
        internal const string PanacheFeatureGuid = "1FC7D950-A017-4C15-AA42-BDB7657174E8";
        internal const string PanacheDisplayName = "Panache.Name";
        internal const string PanacheDescription = "Panache.Description";
        internal const string PanacheDescriptionShort = "Panache.DescriptionShort";

        internal const string Finesse = "SwashbucklerFinesse";
        internal const string FinesseGuid = "A4422438-8C1C-4EDC-B2AB-7BCE5ED9124A";
        internal const string FinesseDisplayName = "Finesse.Name";
        internal const string FinesseDescription = "Finesse.Description";

        internal const string Nimble = "SwashbucklerNimble";
        internal const string NimbleGuid = "EF896BC9-0A5E-4FF7-AAF6-251BE4F9875B";
        internal const string NimbleBuff = "SwashbucklerNimbleBuff";
        internal const string NimbleBuffGuid = "43A17E35-5D72-44F6-8A78-23D142EB61BF";
        internal const string NimbleDisplayName = "Nimble.Name";
        internal const string NimbleDescription = "Nimble.Description";

        internal const string FTraining = "SwashbucklerFTraining";
        internal const string FTrainingGuid = "46279B21-6903-4C8F-9699-2B915D51EC15";
        internal const string FTrainingDisplayName = "FTraining.Name";
        internal const string FTrainingDescription = "FTraining.Description";

        internal const string WTraining = "SwashbucklerWTraining";
        internal const string WTrainingGuid = "4B3C4EFC-D405-4F90-B923-1F02A2B3B884";
        internal const string WTrainingDisplayName = "WTraining.Name";
        internal const string WTrainingDescription = "WTraining.Description";

        internal const string WMastery = "SwashbucklerWMastery";
        internal const string WMasteryGuid = "2B7D02C7-5AB2-4F1E-8BF0-7F6E45598AE0";
        internal const string WMasteryDisplayName = "WMastery.Name";
        internal const string WMasteryDescription = "WMastery.Description";

        internal const string BFeat = "SwashbucklerBFeat";
        internal const string BFeatGuid = "B0A65390-6924-4590-A458-64F049840AD7";
        internal const string BFeatDisplayName = "BFeat.Name";
        internal const string BFeatDescription = "BFeat.Description";

        internal const string CharmedResource = "CharmedLifeResource";
        internal const string CharmedResourceGuid = "E6AD4AD4-C14C-46A9-B18A-F8D9D82A3B33";
        internal const string CharmedAbility = "CharmedLifeAbility";
        internal const string CharmedAbilityGuid = "F7587906-F3C8-490C-9EE4-646780F748C2";
        internal const string CharmedBuff = "CharmedLifeBuff";
        internal const string CharmedBuffGuid = "31E466E6-47B2-489A-BB1A-94E55B0A4D21";
        internal const string CharmedFeature = "CharmedLifeFeature";
        internal const string CharmedFeatureGuid = "EC125AAA-E6F0-42B4-B829-6070A5E773BA";
        internal const string CharmedDisplayName = "CharmedLife.Name";
        internal const string CharmedDescription = "CharmedLife.Description";

        internal const string DerringAbility = "DerringAbility";
        internal const string DerringAbilityGuid = "BE7BD0BE-D940-4648-A98B-E4A60A5678E2";
        internal const string DerringBuff = "DerringBuff";
        internal const string DerringBuffGuid = "3A2CD230-BC48-4B08-A070-37DF7677D6D2";
        internal const string DerringFeature = "DerringFeature";
        internal const string DerringFeatureGuid = "9A939187-3769-4887-ACFA-906DCE81C84C";
        internal const string DerringDisplayName = "Derring.Name";
        internal const string DerringDescription = "Derring.Description";

        internal const string DodgingAbility = "DodgingAbility";
        internal const string DodgingAbilityGuid = "137FF9BA-5DAD-4ED8-A7B1-8106F99FA366";
        internal const string DodgingBuff = "DodgingBuff";
        internal const string DodgingBuffGuid = "85D617F7-148C-4027-8C7F-C8EC859EC6E3";
        internal const string DodgingFeature = "DodgingFeature";
        internal const string DodgingFeatureGuid = "11FEF9EA-1F67-473C-AE69-3106F07FC5B2";
        internal const string DodgingDisplayName = "Dodging.Name";
        internal const string DodgingDescription = "Dodging.Description";

        internal const string SwashParryAbility = "SwashParryAbility";
        internal const string SwashParryAbilityGuid = "57916E6D-9A25-4C11-A77B-5147951D441A";
        internal const string SwashParryBuff = "SwashParryBuff";
        internal const string SwashParryBuffGuid = "4D58AB3F-ADC8-4ACB-9D9C-3F5F90799638";
        internal const string SwashParryFeature = "SwashParryFeature";
        internal const string SwashParryFeatureGuid = "355DBBAF-2E09-472C-AC3F-F903269EA2D8";
        internal const string SwashParryDisplayName = "SwashParry.Name";
        internal const string SwashParryDescription = "SwashParry.Description";

        internal const string KipFeature = "SwashbucklerKip";
        internal const string KipFeatureGuid = "BE98AD50-9A4C-43CE-9BEC-8DE854581AD1";
        internal const string KipAbility = "KipAbility";
        internal const string KipAbilityGuid = "6FBFAFA5-6546-4400-B75B-CC242494BCA2";
        internal const string KipBuff = "KipBuff";
        internal const string KipBuffGuid = "77BE6680-22F0-4A01-8355-57096C736B43";
        internal const string KipDisplayName = "Kip.Name";
        internal const string KipDescription = "Kip.Description";

        internal const string MenacingFeature = "SwashbucklerMenacing";
        internal const string MenacingFeatureGuid = "C1AAA405-A025-4CA4-A2AB-B0B50FFD5C78";
        internal const string MenacingAbility = "MenacingAbility";
        internal const string MenacingAbilityGuid = "003C1AB7-B45C-46A0-9A16-40E4394FF0C1";
        internal const string MenacingBuff = "MenacingBuff";
        internal const string MenacingBuffGuid = "60F92AC6-1BE6-4E16-BDB6-B31E9CDB1041";
        internal const string MenacingDisplayName = "Menacing.Name";
        internal const string MenacingDescription = "Menacing.Description";

        internal const string PreciseFeature = "SwashbucklerPreciseStrike";
        internal const string PreciseFeatureGuid = "714B3AAA-D00C-417D-8F1F-E504AE0DD4B9";
        internal const string PreciseAbility = "SwashbucklerPreciseStrikeAbility";
        internal const string PreciseAbilityGuid = "F47AF60C-4A8C-45A9-AF04-040E472B45D7";
        internal const string PreciseBuff = "SwashbucklerPreciseStrikeBuff";
        internal const string PreciseBuffGuid = "B3BB46CE-DE7F-4A64-897F-ED4E6F5F1F1E";
        internal const string PreciseDisplayName = "Precise.Name";
        internal const string PreciseDescription = "Precise.Description";
        internal const string PreciseAbilityDescription = "PreciseAbility.Description";

        internal const string Initiative = "SwashbucklerInitiative";
        internal const string InitiativeGuid = "ADD89C02-BB1A-4C7D-A5EA-D37451C9949A";
        internal const string InitiativeDisplayName = "Initiative.Name";
        internal const string InitiativeDescription = "Initiative.Description";

        internal const string Grace = "SwashbucklerGrace";
        internal const string GraceGuid = "27B58D1B-E6FF-4C9B-B4A0-708978E8B2B8";
        internal const string GraceAbility = "SwashbucklerGraceAbility";
        internal const string GraceAbilityGuid = "0E724B68-2E7F-454F-ABBA-FB514C81238B";
        internal const string GraceBuff = "SwashbucklerGraceBuff";
        internal const string GraceBuffGuid = "C96EF7DF-7754-41F3-BD73-F82E93DC2AE2";
        internal const string GraceDisplayName = "Grace.Name";
        internal const string GraceDescription = "Grace.Description";
        internal const string GraceAbilityDescription = "GraceAbility.Description";

        internal const string FeintDebuff = "FeintDebuff";
        internal const string FeintDebuffGuid = "B9291969-6985-402B-A84B-9FC71B803226";
        internal const string FeintDebuffDisplayName = "FeintDebuff.Name";
        internal const string FeintDebuffDescription = "FeintDebuff.Description";

        internal const string SupFeint = "SwashbucklerSuperiorFeint";
        internal const string SupFeintGuid = "590C3E64-BE67-4098-98C2-3652B6E16841";
        internal const string SupFeintAbility = "SwashbucklerSuperiorFeintAbility";
        internal const string SupFeintAbilityGuid = "033AA093-DDDF-4967-BB3D-AAB3ABDB629B";
        internal const string SupFeintDisplayName = "SupFeint.Name";
        internal const string SupFeintDescription = "SupFeint.Description";

        internal const string TargetedStrike = "SwashbucklerTargetedStrike";
        internal const string TargetedStrikeGuid = "414C1B70-9220-4183-A37F-2E86B11B5A2E";
        internal const string TargetedStrikeDisplayName = "TargetedStrike.Name";
        internal const string TargetedStrikeDescription = "TargetedStrike.Description";
        internal const string TSAbility = "SwashbucklerTSAbility";
        internal const string TSAbilityGuid = "00EF62F8-DBB0-4F62-B2D0-6790198D832E";

        internal const string TSArms = "SwashbucklerTSArms";
        internal const string TSArmsGuid = "2DC58E1B-8398-4301-9AA9-CE4F514CDB8D";
        internal const string TSArmsDisplayName = "TSArms.Name";
        internal const string TSArmsDescription = "TSArms.Description";

        internal const string TSHead = "SwashbucklerTSHead";
        internal const string TSHeadGuid = "6AAD6010-EB69-49EB-B640-0825D9E9D1C7";
        internal const string TSHeadDisplayName = "TSHead.Name";
        internal const string TSHeadDescription = "TSHead.Description";

        internal const string TSLegs = "SwashbucklerTSLegs";
        internal const string TSLegsGuid = "B0EE55AE-26CB-46F2-817F-75A09645C2A1";
        internal const string TSLegsDisplayName = "TSLegs.Name";
        internal const string TSLegsDescription = "TSLegs.Description";

        internal const string TSTorso = "SwashbucklerTSTorso";
        internal const string TSTorsoGuid = "3A7B14A8-5313-42AC-B356-51F7503C8125";
        internal const string TSTorsoDisplayName = "TSTorso.Name";
        internal const string TSTorsoDescription = "TSTorso.Description";

        internal const string BWoundAbility = "SwashbucklerBWoundAbility";
        internal const string BWoundAbilityGuid = "04657D90-D33D-4729-AC7D-337F7EA0043E";
        internal const string BWound = "SwashbucklerBWound";
        internal const string BWoundGuid = "8FE3F494-BF2B-46CE-A3C0-5B70940C44C4";
        internal const string BWoundDisplayName = "BWound.Name";
        internal const string BWoundDescription = "BWound.Description";

        internal const string BleedDebuff = "SwashbucklerBleedDebuff";
        internal const string BleedDebuffGuid = "A069826C-BB6B-4BAE-9EF3-B0D363E9C3ED";
        internal const string BleedBuff = "SwashbucklerBleedBuff";
        internal const string BleedBuffGuid = "44AD22B7-3E6A-4D2B-8B18-C452BC46F593";
        internal const string BleedAbility = "SwashbucklerBleedAbility";
        internal const string BleedAbilityGuid = "0DF7B61B-AFA2-42D7-BC21-42BCA753A887";
        internal const string BleedDebuffDisplayName = "Bleed.Name";
        internal const string BleedDebuffDescription = "BleedDebuff.Description";
        internal const string BleedAbilityDescription = "BleedAbility.Description";

        internal const string SBleedDebuff = "SwashbucklerSBleedDebuff";
        internal const string SBleedDebuffGuid = "373CB684-6891-48BA-8001-6F9A557C71D7";
        internal const string SBleedBuff = "SwashbucklerSBleedBuff";
        internal const string SBleedBuffGuid = "85F0EFC5-4B54-47EA-88DA-2ECA840D73BC";
        internal const string SBleedAbility = "SwashbucklerSBleedAbility";
        internal const string SBleedAbilityGuid = "2F2DACC3-E0CB-4EF7-A235-00EA599E8E00";
        internal const string SBleedDebuffDisplayName = "SBleed.Name";
        internal const string SBleedDebuffDescription = "SBleedDebuff.Description";
        internal const string SBleedAbilityDescription = "SBleedAbility.Description";

        internal const string DBleedDebuff = "SwashbucklerDBleedDebuff";
        internal const string DBleedDebuffGuid = "7731C314-CF8C-4302-9A15-FA64DA1B3969";
        internal const string DBleedBuff = "SwashbucklerDBleedBuff";
        internal const string DBleedBuffGuid = "9D972DC9-FD2E-4785-8156-80C8BF06BC51";
        internal const string DBleedAbility = "SwashbucklerDBleedAbility";
        internal const string DBleedAbilityGuid = "35AE9A18-EF1C-46B7-86CF-3C8E20776BD2";
        internal const string DBleedDebuffDisplayName = "DBleed.Name";
        internal const string DBleedDebuffDescription = "DBleedDebuff.Description";
        internal const string DBleedAbilityDescription = "DBleedAbility.Description";

        internal const string CBleedDebuff = "SwashbucklerCBleedDebuff";
        internal const string CBleedDebuffGuid = "854A00F0-E00B-4090-B2E2-12697760DA3F";
        internal const string CBleedBuff = "SwashbucklerCBleedBuff";
        internal const string CBleedBuffGuid = "3FA1691D-6694-4224-AE38-C2F112B56040";
        internal const string CBleedAbility = "SwashbucklerCBleedAbility";
        internal const string CBleedAbilityGuid = "B835B48B-1760-419C-9F6F-40173D786C03";
        internal const string CBleedDebuffDisplayName = "CBleed.Name";
        internal const string CBleedDebuffDescription = "CBleedDebuff.Description";
        internal const string CBleedAbilityDescription = "CBleedAbility.Description";

        internal const string Evasive = "SwashbucklerEvasive";
        internal const string EvasiveGuid = "18EC5CF1-8008-4206-86C5-78060903AD45";
        internal const string EvasiveDisplayName = "Evasive.Name";
        internal const string EvasiveDescription = "Evasive.Description";

        internal const string Subtle = "SwashbucklerSubtle";
        internal const string SubtleGuid = "E170A4DF-F533-4A4A-BB2C-9864C25AE99F";
        internal const string SubtleDisplayName = "Subtle.Name";
        internal const string SubtleDescription = "Subtle.Description";

        internal const string DizzyingFeature = "SwashbucklerDizzying";
        internal const string DizzyingFeatureGuid = "5E2EC03C-3EC3-4046-A9D2-FD3D1C195594";
        internal const string DizzyingAbility = "DizzyingAbility";
        internal const string DizzyingAbilityGuid = "2E30DC1D-778F-4DEE-B9F8-6157947BE725";
        internal const string DizzyingBuff = "DizzyingBuff";
        internal const string DizzyingBuffGuid = "3187B37D-C2F4-45DD-8BB8-D7B0E406734A";
        internal const string DizzyingDisplayName = "Dizzying.Name";
        internal const string DizzyingDescription = "Dizzying.Description";

        internal const string PerfectThrustFeature = "SwashbucklerPerfectThrust";
        internal const string PerfectThrustFeatureGuid = "B59AF802-7958-44EA-BC13-6F2BFF5D8516";
        internal const string PerfectThrustAbility = "PerfectThrustAbility";
        internal const string PerfectThrustAbilityGuid = "5015B851-7CB9-4B25-98CA-C38DF739B44F";
        internal const string PerfectThrustBuff = "PerfectThrustBuff";
        internal const string PerfectThrustBuffGuid = "4F75B433-FC90-469F-A910-F2D5BED2F6BA";
        internal const string PerfectThrustDisplayName = "PerfectThrust.Name";
        internal const string PerfectThrustDescription = "PerfectThrust.Description";

        internal const string EdgeFeature = "SwashbucklerEdge";
        internal const string EdgeFeatureGuid = "570DD103-7342-439B-B5FB-2C84A1FCA98A";
        internal const string EdgeAbility = "EdgeAbility";
        internal const string EdgeAbilityGuid = "2B6081EC-8EE9-48DD-9EF3-70B50796B194";
        internal const string EdgeBuff = "EdgeBuff";
        internal const string EdgeBuffGuid = "30E218AC-CDC1-4737-B090-5C7A2F354BDF";
        internal const string EdgeDisplayName = "Edge.Name";
        internal const string EdgeDescription = "Edge.Description";

        internal const string CheatDeathFeature = "SwashbucklerCheatDeath";
        internal const string CheatDeathFeatureGuid = "25BE53F3-5BB5-45AE-844E-058E93335988";
        internal const string CheatDeathAbility = "CheatDeathAbility";
        internal const string CheatDeathAbilityGuid = "23431F92-BC1C-4896-89F9-0E4DE9FE713D";
        internal const string CheatDeathBuff = "CheatDeathBuff";
        internal const string CheatDeathBuffGuid = "D9D00D57-3483-49D9-B232-A0AC67FD49FF";
        internal const string CheatDeathDisplayName = "CheatDeath.Name";
        internal const string CheatDeathDescription = "CheatDeath.Description";

        internal const string DeadlyFeature = "SwashbucklerDeadly";
        internal const string DeadlyFeatureGuid = "2CEF8A28-F7CF-4098-9008-4369142A9F02";
        internal const string DeadlyAbility = "DeadlyAbility";
        internal const string DeadlyAbilityGuid = "42D0242B-C734-47AF-A047-667C39B0EF18";
        internal const string DeadlyBuff = "DeadlyBuff";
        internal const string DeadlyBuffGuid = "EFA3DE15-9686-40E1-BA79-D59B0E776EF9";
        internal const string DeadlyDisplayName = "Deadly.Name";
        internal const string DeadlyDescription = "Deadly.Description";

        internal const string StunningFeature = "SwashbucklerStunning";
        internal const string StunningFeatureGuid = "7D81A096-FF2F-450D-AED1-4BE04B8466CC";
        internal const string StunningAbility = "StunningAbility";
        internal const string StunningAbilityGuid = "A63B3F78-B2E0-4FCD-9E1F-07D38772D7F7";
        internal const string StunningBuff = "StunningBuff";
        internal const string StunningBuffGuid = "71B88823-FC91-4618-B6EC-5BAA345E922E";
        internal const string StunningDisplayName = "Stunning.Name";
        internal const string StunningDescription = "Stunning.Description";

        internal const string Deeds1 = "Deeds1";
        internal const string Deeds1Guid = "90DDA101-134D-4F44-8E3A-A429A7777E91";
        internal const string Deeds3 = "Deeds3";
        internal const string Deeds3Guid = "EE95ED61-1F39-49C9-BADE-23ECC3CD2CBB";
        internal const string Deeds7 = "Deeds7";
        internal const string Deeds7Guid = "B7A8612D-FF55-4416-BEE3-97CCAF787278";
        internal const string Deeds11 = "Deeds11";
        internal const string Deeds11Guid = "8631E60C-28A6-4441-A963-861EE5D5B398";
        internal const string Deeds15 = "Deeds15";
        internal const string Deeds15Guid = "B569948F-AE1A-4F97-B694-E8AAD786E910";
        internal const string Deeds19 = "Deeds19";
        internal const string Deeds19Guid = "C4997A33-FBD1-46AC-BC00-03FA8F7943A1";
        internal const string DeedsDisplayName = "Deeds.Name";
        internal const string DeedsDescription = "Deeds.Description";

        internal static void Configure()
        {
            swash_class = CharacterClassConfigurator.New(SwashName, SwashGuid)
                .SetLocalizedName(SwashDisplayName)
                .SetLocalizedDescription(SwashDescription)
                .SetLocalizedDescriptionShort(SwashDescription)
                .SetBaseAttackBonus(StatProgressionRefs.BABFull.Reference.Get())
                .SetSkillPoints(4)
                .SetHitDie(DiceType.D10)
                .SetFortitudeSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetClassSkills(StatType.SkillMobility, StatType.SkillPersuasion, StatType.SkillKnowledgeWorld, StatType.SkillPerception, StatType.SkillAthletics, StatType.SkillThievery)
                .SetRecommendedAttributes(StatType.Dexterity, StatType.Charisma)
                .SetIsArcaneCaster(false)
                .SetIsDivineCaster(false)
                .SetSpellbook(null)
                .SetDefaultBuild(null)
                .SetPrestigeClass(false)
                .SetDifficulty(4)
                .AddToStartingItems(ItemWeaponRefs.ColdIronRapier.Reference.Get(), ItemArmorRefs.LeatherStandard.Reference.Get(), ItemShieldRefs.Buckler.Reference.Get(), ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get())
                .SetStartingGold(411)
                .Configure();

            swash_class.MaleEquipmentEntities = CharacterClassRefs.SlayerClass.Reference.Get().MaleEquipmentEntities;
            swash_class.FemaleEquipmentEntities = CharacterClassRefs.SlayerClass.Reference.Get().FemaleEquipmentEntities;
            swash_class.PrimaryColor = CharacterClassRefs.MagusClass.Reference.Get().PrimaryColor;
            swash_class.SecondaryColor = CharacterClassRefs.MagusClass.Reference.Get().SecondaryColor;

            panache_feature = CreatePanache();
            swash_finesse = CreateFinesse();
            charmed_life = CreateCharmed();
            var profs = CreateProficiencies();
            swash_bonus_feats = CreateBonusFeat();
            swash_weapon_training = CreateWeaponTraining();
            swash_weapon_mastery = CreateWeaponMastery();
            nimble = CreateNimble();
            var ftraining = CreateFighterTraining();
            deeds3 = CreateDeeds3();
            deeds7 = CreateDeeds7();
            deeds11 = CreateDeeds11();
            var deeds15 = CreateDeeds15();
            var deeds19 = CreateDeeds19();


            Archetypes.InspiredBlade.Configure();
            Archetypes.Azatariel.Configure();

            var deeds1 = CreateDeeds1();

            var lb = new LevelEntryBuilder();
            lb.AddEntry(1, deeds1, panache_feature, swash_finesse, profs);
            lb.AddEntry(2, charmed_life);
            lb.AddEntry(3, nimble, deeds3);
            lb.AddEntry(4, ftraining, swash_bonus_feats);
            lb.AddEntry(5, swash_weapon_training);
            lb.AddEntry(7, deeds7);
            lb.AddEntry(8, swash_bonus_feats);
            lb.AddEntry(9, swash_weapon_training);
            lb.AddEntry(11, deeds11);
            lb.AddEntry(13, swash_weapon_training);
            lb.AddEntry(12, swash_bonus_feats);
            lb.AddEntry(15, deeds15);
            lb.AddEntry(16, swash_bonus_feats);
            lb.AddEntry(17, swash_weapon_training);
            lb.AddEntry(19, deeds19);
            lb.AddEntry(20, swash_bonus_feats, swash_weapon_mastery);

            var ui = new UIGroupBuilder();
            ui.AddGroup(swash_bonus_feats);
            ui.AddGroup(swash_weapon_training, swash_weapon_mastery);
            ui.AddGroup(Archetypes.InspiredBlade.rapier_training, Archetypes.InspiredBlade.rapier_mastery);
            ui.AddGroup(deeds1, deeds3, deeds7, deeds11, deeds15, deeds19);
            ui.AddGroup(Archetypes.Azatariel.azata_deeds3, Archetypes.Azatariel.azata_deeds7, Archetypes.Azatariel.azata_deeds11);
            ui.SetGroupDeterminators(panache_feature, swash_finesse, profs, Archetypes.InspiredBlade.inspired_panache, Archetypes.InspiredBlade.inspired_finesse);

            var prog = ProgressionConfigurator.New(Progression, ProgressionGuid)
                .AddToLevelEntries(lb.GetEntries())
                .SetUIGroups(ui)
                .SetClasses(swash_class)
                .Configure();

            swash_class = CharacterClassConfigurator.For(swash_class)
                .SetProgression(prog)
                .SetSignatureAbilities(new Blueprint<BlueprintFeatureReference>[] { panache_feature, deeds1, swash_weapon_training })
                .Configure(delayed: true);

            RootConfigurator.For(RootRefs.BlueprintRoot)
                .ModifyProgression(c => c.m_CharacterClasses = c.m_CharacterClasses.AddToArray(swash_class.ToReference<BlueprintCharacterClassReference>()))
                .Configure(delayed: true);
        }

        internal static BlueprintFeature CreateProficiencies()
        {
            return FeatureConfigurator.New(Proficiencies, ProficienciesGuid)
              .SetDisplayName(ProficienciesDisplayName)
              .SetDescription(ProficienciesDescription)
              .AddFacts(new()
              {
                  FeatureRefs.SimpleWeaponProficiency.Reference.Get(),
                  FeatureRefs.MartialWeaponProficiency.Reference.Get(),
                  FeatureRefs.LightArmorProficiency.Reference.Get(),
                  FeatureRefs.BucklerProficiency.Reference.Get()
              })
              .SetIsClassFeature()
              .Configure();
        }

        internal static BlueprintFeature CreatePanache()
        {
            panache_resource = AbilityResourceConfigurator.New(PanacheResource, PanacheResourceGuid)
                .SetMaxAmount(ResourceAmountBuilder.New(0).IncreaseByStat(StatType.Charisma))
                .SetMin(1)
                .Configure();

            return FeatureConfigurator.New(PanacheFeature, PanacheFeatureGuid)
                .SetDisplayName(PanacheDisplayName)
                .SetDescription(PanacheDescription)
                .SetDescriptionShort(PanacheDescriptionShort)
                .SetIcon(FeatureRefs.Bravery.Reference.Get().Icon)
                .AddAbilityResources(resource: panache_resource, restoreAmount: true)
                .AddInitiatorAttackWithWeaponTrigger(action: ActionsBuilder.New().RestoreResource(panache_resource, 1), actionsOnInitiator: true, duelistWeapon: true, criticalHit: true)
                .AddInitiatorAttackWithWeaponTrigger(action: ActionsBuilder.New().RestoreResource(panache_resource, 1), actionsOnInitiator: true, duelistWeapon: true, reduceHPToZero: true)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateFinesse()
        {
            var swash_finesse = FeatureConfigurator.New(Finesse, FinesseGuid)
                .SetDisplayName(FinesseDisplayName)
                .SetDescription(FinesseDescription)
                .SetIcon(FeatureRefs.WeaponFinesse.Reference.Get().Icon)
                .AddComponent<AttackStatReplacementForSwashbucklerWeapon>()
                .AddReplaceStatForPrerequisites(StatType.Charisma, StatType.Intelligence)
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureRefs.WeaponFinesse.Reference.deserializedGuid } })
                .AddRecommendationNoFeatFromGroup(new() { FeatureRefs.WeaponFinesse.Reference.Get() })
                .SetIsClassFeature()
                .Configure();

            FeatureConfigurator.For(FeatureRefs.WeaponFinesse)
                .AddRecommendationNoFeatFromGroup(new() { swash_finesse })
                .Configure();

            return swash_finesse;
        }

        internal static BlueprintFeature CreateCharmed()
        {
            var charmed_resource = AbilityResourceConfigurator.New(CharmedResource, CharmedResourceGuid)
                .SetMaxAmount(ResourceAmountBuilder.New(0).IncreaseByLevelStartPlusDivStep(classes: new string[] { SwashName }, startingLevel: 2, startingBonus: 3, levelsPerStep: 4, bonusPerStep: 1, minBonus: 0))
                .Configure();

            var charmed_buff = BuffConfigurator.New(CharmedBuff, CharmedBuffGuid)
                .SetDisplayName(CharmedDisplayName)
                .SetDescription(CharmedDescription)
                .SetIcon(FeatureRefs.WitchHexFortuneFeature.Reference.Get().Icon)
                .AddNotDispelable()
                .AddDerivativeStatBonus(baseStat: StatType.Charisma, derivativeStat: StatType.SaveFortitude)
                .AddDerivativeStatBonus(baseStat: StatType.Charisma, derivativeStat: StatType.SaveReflex)
                .AddDerivativeStatBonus(baseStat: StatType.Charisma, derivativeStat: StatType.SaveWill)
                .AddRecalculateOnStatChange(stat: StatType.Charisma)
                .AddInitiatorSavingThrowTrigger(ActionsBuilder.New().ContextSpendResource(resource: charmed_resource, value: ContextValues.Constant(1)))
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = charmed_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var charmed_ability = ActivatableAbilityConfigurator.New(CharmedAbility, CharmedAbilityGuid)
                .SetDisplayName(CharmedDisplayName)
                .SetDescription(CharmedDescription)
                .SetIcon(FeatureRefs.WitchHexFortuneFeature.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: charmed_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(charmed_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(CharmedFeature, CharmedFeatureGuid)
                .SetDisplayName(CharmedDisplayName)
                .SetDescription(CharmedDescription)
                .SetIcon(FeatureRefs.WitchHexFortuneFeature.Reference.Get().Icon)
                .AddAbilityResources(resource: charmed_resource, restoreAmount: true)
                .AddFacts(new() { charmed_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateNimble()
        {
            var nimble_buff = BuffConfigurator.New(NimbleBuff, NimbleBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddContextRankConfig(ContextRankConfigs.ClassLevel(classes: new string[] { SwashName }).WithStartPlusDivStepProgression(start: 3, divisor: 4, delayStart: true))
                .AddContextStatBonus(stat: StatType.AC, descriptor: ModifierDescriptor.Dodge, value: ContextValues.Rank())
                .Configure();

            return FeatureConfigurator.New(Nimble, NimbleGuid)
                .SetDisplayName(NimbleDisplayName)
                .SetDescription(NimbleDescription)
                .SetIcon(FeatureRefs.ArmorTraining.Reference.Get().Icon)
                .AddBuffOnLightOrNoArmor(nimble_buff)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateFighterTraining()
        {
            return FeatureConfigurator.New(FTraining, FTrainingGuid)
                .SetDisplayName(FTrainingDisplayName)
                .SetDescription(FTrainingDescription)
                .SetIcon(FeatureRefs.FighterTraining.Reference.Get().Icon)
                .AddClassLevelsForPrerequisites(actualClass: SwashName, fakeClass: CharacterClassRefs.FighterClass.Reference.Get(), modifier: 1)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateBonusFeat()
        {
            return FeatureSelectionConfigurator.New(BFeat, BFeatGuid)
                .CopyFrom(FeatureSelectionRefs.FighterFeatSelection)
                .SetDisplayName(BFeatDisplayName)
                .SetDescription(BFeatDescription)
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateWeaponTraining()
        {
            var swash_training = FeatureConfigurator.New(WTraining, WTrainingGuid)
                .SetDisplayName(WTrainingDisplayName)
                .SetDescription(WTrainingDescription)
                .SetIcon(FeatureRefs.WeaponMastery.Reference.Get().Icon)
                .AddComponent<ImprovedCriticalOnWieldingSwashbucklerWeapon>()
                .AddWeaponTraining()
                .AddWeaponTrainingBonuses(stat: StatType.AdditionalAttackBonus, descriptor: ModifierDescriptor.UntypedStackable)
                .AddWeaponTrainingBonuses(stat: StatType.AdditionalDamage, descriptor: ModifierDescriptor.UntypedStackable)
                .AddComponent(new FeatureForPrerequisite() { FakeFact = new BlueprintUnitFactReference() { deserializedGuid = FeatureSelectionRefs.WeaponTrainingSelection.Reference.deserializedGuid } })
                .AddToGroups(FeatureGroup.WeaponTraining)
                .AddContextRankConfig(ContextRankConfigs.FeatureRank(WTraining))
                .SetReapplyOnLevelUp()
                .SetRanks(4)
                .SkipAddToSelections()
                .SetIsClassFeature()
                .Configure();

            ParametrizedFeatureConfigurator.For(ParametrizedFeatureRefs.ImprovedCritical)
                .AddRecommendationNoFeatFromGroup(new() { swash_training })
                .Configure();

            return swash_training;
        }

        internal static BlueprintFeature CreateWeaponMastery()
        {
            return FeatureConfigurator.New(WMastery, WMasteryGuid)
                .SetDisplayName(WMasteryDisplayName)
                .SetDescription(WMasteryDescription)
                .SetIcon(ParametrizedFeatureRefs.WeaponMasteryParametrized.Reference.Get().Icon)
                .AddComponent<CritAutoconfirmWithSwashbucklerWeapons>()
                .AddComponent<IncreasedCriticalMultiplierWithSwashbucklerWeapon>()
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDerringDo()
        {
            var derring_buff = BuffConfigurator.New(DerringBuff, DerringBuffGuid)
                .SetDisplayName(DerringDisplayName)
                .SetDescription(DerringDescription)
                .SetIcon(FeatureRefs.BloodlineFeyWoodlandStride.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<DerringDo>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var derring_ability = ActivatableAbilityConfigurator.New(DerringAbility, DerringAbilityGuid)
                .SetDisplayName(DerringDisplayName)
                .SetDescription(DerringDescription)
                .SetIcon(FeatureRefs.BloodlineFeyWoodlandStride.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(derring_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(DerringFeature, DerringFeatureGuid)
                .SetDisplayName(DerringDisplayName)
                .SetDescription(DerringDescription)
                .SetIcon(FeatureRefs.BloodlineFeyWoodlandStride.Reference.Get().Icon)
                .AddFacts(new() { derring_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDodgingPanache()
        {
            var dodging_buff = BuffConfigurator.New(DodgingBuff, DodgingBuffGuid)
                .SetDisplayName(DodgingDisplayName)
                .SetDescription(DodgingDescription)
                .SetIcon(AbilityRefs.MirrorImage.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<DodgingPanache>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var dodging_ability = ActivatableAbilityConfigurator.New(DodgingAbility, DodgingAbilityGuid)
                .SetDisplayName(DodgingDisplayName)
                .SetDescription(DodgingDescription)
                .SetIcon(AbilityRefs.MirrorImage.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(dodging_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(DodgingFeature, DodgingFeatureGuid)
                .SetDisplayName(DodgingDisplayName)
                .SetDescription(DodgingDescription)
                .SetIcon(AbilityRefs.MirrorImage.Reference.Get().Icon)
                .AddFacts(new() { dodging_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateParry()
        {
            var parry_buff = BuffConfigurator.New(SwashParryBuff, SwashParryBuffGuid)
                .SetDisplayName(SwashParryDisplayName)
                .SetDescription(SwashParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<ParryAndRiposte>()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var parry_ability = ActivatableAbilityConfigurator.New(SwashParryAbility, SwashParryAbilityGuid)
                .SetDisplayName(SwashParryDisplayName)
                .SetDescription(SwashParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(parry_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(SwashParryFeature, SwashParryFeatureGuid)
                .SetDisplayName(SwashParryDisplayName)
                .SetDescription(SwashParryDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistParrySelfAbility.Reference.Get().Icon)
                .AddFacts(new() { parry_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds1()
        {
            return FeatureConfigurator.New(Deeds1, Deeds1Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateDerringDo(), CreateDodgingPanache(), CreateParry() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateKipUp()
        {
            kip_buff = BuffConfigurator.New(KipBuff, KipBuffGuid)
                .SetDisplayName(KipDisplayName)
                .SetDescription(KipDescription)
                .SetIcon(AbilityRefs.TouchOfGracelessness.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var kip_ability = ActivatableAbilityConfigurator.New(KipAbility, KipAbilityGuid)
                .SetDisplayName(KipDisplayName)
                .SetDescription(KipDescription)
                .SetIcon(AbilityRefs.TouchOfGracelessness.Reference.Get().Icon)
                .AddActivatableAbilityResourceLogic(requiredResource: panache_resource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Never)
                .SetBuff(kip_buff)
                .SetDeactivateImmediately()
                .Configure();

            kip_feature = FeatureConfigurator.New(KipFeature, KipFeatureGuid)
                .SetDisplayName(KipDisplayName)
                .SetDescription(KipDescription)
                .SetIcon(AbilityRefs.TouchOfGracelessness.Reference.Get().Icon)
                .AddFacts(new() { kip_ability })
                .AddComponent<KipUp>()
                .SetIsClassFeature()
                .Configure();

            return kip_feature;
        }

        internal static BlueprintFeature CreateMenacing()
        {
            var menacing_buff = BuffConfigurator.New(MenacingBuff, MenacingBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<MenacingSwordplay>()
                .Configure();

            var menacing_ability = ActivatableAbilityConfigurator.New(MenacingAbility, MenacingAbilityGuid)
                .SetDisplayName(MenacingDisplayName)
                .SetDescription(MenacingDescription)
                .SetIcon(FeatureRefs.ShatterConfidence.Reference.Get().Icon)
                .SetBuff(menacing_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(MenacingFeature, MenacingFeatureGuid)
                .SetDisplayName(MenacingDisplayName)
                .SetDescription(MenacingDescription)
                .SetIcon(FeatureRefs.ShatterConfidence.Reference.Get().Icon)
                .AddFacts(new() { menacing_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreatePrecise()
        {
            precise_strike_buff = BuffConfigurator.New(PreciseBuff, PreciseBuffGuid)
                .SetDisplayName(PreciseDisplayName)
                .SetDescription(PreciseAbilityDescription)
                .AddNotDispelable()
                .SetIcon(FeatureRefs.PreciseStrikeAbility.Reference.Get().Icon)
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .AddRemoveBuffOnAttack()
                .Configure();

            var precise_ability = AbilityConfigurator.New(PreciseAbility, PreciseAbilityGuid)
                .SetDisplayName(PreciseDisplayName)
                .SetDescription(PreciseAbilityDescription)
                .SetIcon(FeatureRefs.PreciseStrikeAbility.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .SetRange(AbilityRange.Personal)
                .SetAnimation(UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .SetActionType(UnitCommand.CommandType.Swift)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .ApplyBuff(precise_strike_buff, ContextDuration.Fixed(1)))
                .AddAbilityResourceLogic(requiredResource: panache_resource, amount: 1, isSpendResource: true)
                .Configure();

            return FeatureConfigurator.New(PreciseFeature, PreciseFeatureGuid)
                .SetDisplayName(PreciseDisplayName)
                .SetDescription(PreciseDescription)
                .SetIcon(FeatureRefs.PreciseStrikeAbility.Reference.Get().Icon)
                .AddFacts(new() { precise_ability })
                .AddComponent<SwashbucklerPreciseStrike>()
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateInitiative()
        {
            swash_init_feature = FeatureConfigurator.New(Initiative, InitiativeGuid)
                .SetDisplayName(InitiativeDisplayName)
                .SetDescription(InitiativeDescription)
                .SetIcon(FeatureRefs.Evasion.Reference.Get().Icon)
                .AddComponent<SwashbucklerInitiative>()
                .SetIsClassFeature()
                .Configure();

            return swash_init_feature;
        }

        internal static BlueprintFeature CreateDeeds3()
        {
            return FeatureConfigurator.New(Deeds3, Deeds3Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateKipUp(), CreateMenacing(), CreatePrecise(), CreateInitiative() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateGrace()
        {
            var grace_buff = BuffConfigurator.New(GraceBuff, GraceBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<SwashbucklerGrace>()
                .Configure();

            var grace_ability = ActivatableAbilityConfigurator.New(GraceAbility, GraceAbilityGuid)
                .SetDisplayName(GraceDisplayName)
                .SetDescription(GraceAbilityDescription)
                .SetIcon(ActivatableAbilityRefs.MobilityUseAbility.Reference.Get().Icon)
                .SetBuff(grace_buff)
                .SetDeactivateImmediately()
                .Configure();

            grace_feat = FeatureConfigurator.New(Grace, GraceGuid)
                .SetDisplayName(GraceDisplayName)
                .SetDescription(GraceDescription)
                .SetIcon(ActivatableAbilityRefs.MobilityUseAbility.Reference.Get().Icon)
                .AddFacts(new() { grace_ability })
                .SetIsClassFeature()
                .Configure();

            return grace_feat;
        }

        internal static BlueprintFeature CreateSupFeint()
        {
            var superior_feint_ability = AbilityConfigurator.New(SupFeintAbility, SupFeintAbilityGuid)
                .SetDisplayName(SupFeintDisplayName)
                .SetDescription(SupFeintDescription)
                .SetIcon(FeatureRefs.SwordlordDefensiveParryFeature.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetActionType(UnitCommand.CommandType.Standard)
                .SetRange(AbilityRange.Weapon)
                .AddAbilityEffectRunAction(Feats.FeintFeats.feint_action)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityCasterHasAtLeastOnePanache>()
                .Configure();

            sup_feint_feat = FeatureConfigurator.New(SupFeint, SupFeintGuid)
                .SetDisplayName(SupFeintDisplayName)
                .SetDescription(SupFeintDescription)
                .SetIcon(FeatureRefs.SwordlordDefensiveParryFeature.Reference.Get().Icon)
                .AddFacts(new() { superior_feint_ability })
                .SetIsClassFeature()
                .Configure();

            return sup_feint_feat;
        }

        internal static BlueprintFeature CreateTargetedStrike()
        {
            var arms_ability = AbilityConfigurator.New(TSArms, TSArmsGuid)
                .SetDisplayName(TSArmsDisplayName)
                .SetDescription(TSArmsDescription)
                .SetIcon(AbilityRefs.StunningFistFatigueAbility.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetIsFullRoundAction()
                .SetRange(AbilityRange.Weapon)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityEffectRunAction(ActionsBuilder.New().Add<Disarm>().Build())
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityTargetNotImmuneToCritical>()
                .AddComponent<AbilityTargetNotImmuneToPrecision>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityResourceLogic(requiredResource: panache_resource, isSpendResource: true, amount: 1)
                .Configure();

            var head_ability = AbilityConfigurator.New(TSHead, TSHeadGuid)
                .SetDisplayName(TSHeadDisplayName)
                .SetDescription(TSHeadDescription)
                .SetIcon(FeatureRefs.SlipperyMind.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetIsFullRoundAction()
                .SetRange(AbilityRange.Weapon)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(BuffRefs.Confusion.Reference.Get(), ContextDuration.Fixed(1)).Build())
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityTargetNotImmuneToCritical>()
                .AddComponent<AbilityTargetNotImmuneToPrecision>()
                .AddComponent<AbilityTargetNotImmuneToMindAffecting>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityResourceLogic(requiredResource: panache_resource, isSpendResource: true, amount: 1)
            .Configure();

            var legs_ability = AbilityConfigurator.New(TSLegs, TSLegsGuid)
                .SetDisplayName(TSLegsDisplayName)
                .SetDescription(TSLegsDescription)
                .SetIcon(FeatureRefs.Trailblazer.Reference.Get().Icon)
                .SetIcon(AbilityRefs.Grease.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetIsFullRoundAction()
                .SetRange(AbilityRange.Weapon)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityEffectRunAction(ActionsBuilder.New().KnockdownTarget().Build())
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityTargetNotImmuneToCritical>()
                .AddComponent<AbilityTargetNotImmuneToPrecision>()
                .AddComponent<AbilityTargetNotImmuneToProne>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityResourceLogic(requiredResource: panache_resource, isSpendResource: true, amount: 1)
                .Configure();

            var torso_ability = AbilityConfigurator.New(TSTorso, TSTorsoGuid)
                .SetDisplayName(TSTorsoDisplayName)
                .SetDescription(TSTorsoDescription)
                .SetIcon(AbilityRefs.EnlargePerson.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetIsFullRoundAction()
                .SetRange(AbilityRange.Weapon)
                .SetAnimation(CastAnimationStyle.Immediate)
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(BuffRefs.Staggered.Reference.Get(), ContextDuration.Fixed(1)).Build())
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityTargetNotImmuneToCritical>()
                .AddComponent<AbilityTargetNotImmuneToPrecision>()
                .AddComponent<AbilityDeliverAttackWithWeaponOnHit>()
                .AddAbilityResourceLogic(requiredResource: panache_resource, isSpendResource: true, amount: 1)
                .Configure();

            var targeted_strike_ability = AbilityConfigurator.New(TSAbility, TSAbilityGuid)
                .SetDisplayName(TargetedStrikeDisplayName)
                .SetDescription(TargetedStrikeDescription)
                .SetIcon(FeatureRefs.TwoHandedFighterBackswing.Reference.Get().Icon)
                .AddAbilityVariants(new() { arms_ability, head_ability, legs_ability, torso_ability })
                .SetIsFullRoundAction()
                .Configure();

            return FeatureConfigurator.New(TargetedStrike, TargetedStrikeGuid)
                .SetDisplayName(TargetedStrikeDisplayName)
                .SetDescription(TargetedStrikeDescription)
                .SetIcon(FeatureRefs.TwoHandedFighterBackswing.Reference.Get().Icon)
                .AddFacts(new() { targeted_strike_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds7()
        {
            return FeatureConfigurator.New(Deeds7, Deeds7Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateGrace(), CreateSupFeint(), CreateTargetedStrike() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateBleedingWound()
        {
            var bleeding_wound_group = ExpandedActivatableAbilityGroup.BleedingWound;

            var bleed_debuff = BuffConfigurator.New(BleedDebuff, BleedDebuffGuid)
                .CopyFrom(
                    BuffRefs.Bleed1d4Buff,
                    typeof(AddHealTrigger),
                    typeof(SpellDescriptorComponent),
                    typeof(CombatStateTrigger))
                .SetDisplayName(BleedDebuffDisplayName)
                .SetDescription(BleedDebuffDescription)
                .SetIcon(BuffRefs.Bleed1d4Buff.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddFactContextActions>(c => { c.NewRound = ActionsBuilder.New().Add<ContextActionDealDamage>(c => { c.m_Type = ContextActionDealDamage.Type.Damage; c.DamageType = DamageTypes.Direct(); c.Value = ContextDice.Value(DiceType.Zero, diceCount: 0, bonus: ContextValues.Property(UnitProperty.StatBonusDexterity, true)); }).Build(); })
                .Configure();

            var strength_bleed_debuff = BuffConfigurator.New(SBleedDebuff, SBleedDebuffGuid)
                .CopyFrom(
                    BuffRefs.Bleed1d4Buff,
                    typeof(AddHealTrigger),
                    typeof(SpellDescriptorComponent),
                    typeof(CombatStateTrigger))
                .SetDisplayName(SBleedDebuffDisplayName)
                .SetDescription(SBleedDebuffDescription)
                .SetIcon(BuffRefs.StrengthDamage.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddFactContextActions>(c => { c.NewRound = ActionsBuilder.New().Add<ContextActionDealDamage>(c => { c.m_Type = ContextActionDealDamage.Type.AbilityDamage; c.AbilityType = StatType.Strength; c.Value = ContextDice.Value(DiceType.Zero, diceCount: 0, bonus: ContextValues.Constant(1)); }).Build(); })
                .Configure();

            var dexterity_bleed_debuff = BuffConfigurator.New(DBleedDebuff, DBleedDebuffGuid)
                .CopyFrom(
                    BuffRefs.Bleed1d4Buff,
                    typeof(AddHealTrigger),
                    typeof(SpellDescriptorComponent),
                    typeof(CombatStateTrigger))
                .SetDisplayName(DBleedDebuffDisplayName)
                .SetDescription(DBleedDebuffDescription)
                .SetIcon(BuffRefs.DexterityDamage.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddFactContextActions>(c => { c.NewRound = ActionsBuilder.New().Add<ContextActionDealDamage>(c => { c.m_Type = ContextActionDealDamage.Type.AbilityDamage; c.AbilityType = StatType.Dexterity; c.Value = ContextDice.Value(DiceType.Zero, diceCount: 0, bonus: ContextValues.Constant(1)); }).Build(); })
                .Configure();

            var constitution_bleed_debuff = BuffConfigurator.New(CBleedDebuff, CBleedDebuffGuid)
                .CopyFrom(
                    BuffRefs.Bleed1d4Buff,
                    typeof(AddHealTrigger),
                    typeof(SpellDescriptorComponent),
                    typeof(CombatStateTrigger))
                .SetDisplayName(CBleedDebuffDisplayName)
                .SetDescription(CBleedDebuffDescription)
                .SetIcon(BuffRefs.ConstitutionDamage.Reference.Get().Icon)
                .AddNotDispelable()
                .AddComponent<AddFactContextActions>(c => { c.NewRound = ActionsBuilder.New().Add<ContextActionDealDamage>(c => { c.m_Type = ContextActionDealDamage.Type.AbilityDamage; c.AbilityType = StatType.Constitution; c.Value = ContextDice.Value(DiceType.Zero, diceCount: 0, bonus: ContextValues.Constant(1)); }).Build(); })
                .Configure();

            var bleed_buff = BuffConfigurator.New(BleedBuff, BleedBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, onlyHit: true, action: ActionsBuilder.New().Conditional(ConditionsBuilder.New().HasBuff(bleed_debuff).Build(), ifFalse: ActionsBuilder.New().ApplyBuffPermanent(bleed_debuff).ContextSpendResource(panache_resource, 1).Build()))
                .Configure();

            var strength_bleed_buff = BuffConfigurator.New(SBleedBuff, SBleedBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, onlyHit: true, action: ActionsBuilder.New().Conditional(ConditionsBuilder.New().HasBuff(strength_bleed_debuff).Build(), ifFalse: ActionsBuilder.New().ApplyBuffPermanent(strength_bleed_debuff).ContextSpendResource(panache_resource, 2).Build()))
                .Configure();

            var dexterity_bleed_buff = BuffConfigurator.New(DBleedBuff, DBleedBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, onlyHit: true, action: ActionsBuilder.New().Conditional(ConditionsBuilder.New().HasBuff(dexterity_bleed_debuff).Build(), ifFalse: ActionsBuilder.New().ApplyBuffPermanent(dexterity_bleed_debuff).ContextSpendResource(panache_resource, 2).Build()))
                .Configure();

            var constitution_bleed_buff = BuffConfigurator.New(CBleedBuff, CBleedBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, onlyHit: true, action: ActionsBuilder.New().Conditional(ConditionsBuilder.New().HasBuff(constitution_bleed_debuff).Build(), ifFalse: ActionsBuilder.New().ApplyBuffPermanent(constitution_bleed_debuff).ContextSpendResource(panache_resource, 2).Build()))
                .Configure();

            var bleed_ability = ActivatableAbilityConfigurator.New(BleedAbility, BleedAbilityGuid)
                .SetDisplayName(BleedDebuffDisplayName)
                .SetDescription(BleedAbilityDescription)
                .SetIcon(BuffRefs.Bleed1d4Buff.Reference.Get().Icon)
                .SetBuff(bleed_buff)
                .SetDeactivateImmediately()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .SetHiddenInUI()
                .SetGroup(bleeding_wound_group)
                .Configure();

            var strength_bleed_ability = ActivatableAbilityConfigurator.New(SBleedAbility, SBleedAbilityGuid)
                .SetDisplayName(SBleedDebuffDisplayName)
                .SetDescription(SBleedAbilityDescription)
                .SetIcon(BuffRefs.StrengthDamage.Reference.Get().Icon)
                .SetBuff(strength_bleed_buff)
                .SetDeactivateImmediately()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .SetHiddenInUI()
                .SetGroup(bleeding_wound_group)
                .Configure();

            var dexterity_bleed_ability = ActivatableAbilityConfigurator.New(DBleedAbility, DBleedAbilityGuid)
                .SetDisplayName(DBleedDebuffDisplayName)
                .SetDescription(DBleedAbilityDescription)
                .SetIcon(BuffRefs.DexterityDamage.Reference.Get().Icon)
                .SetBuff(dexterity_bleed_buff)
                .SetDeactivateImmediately()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .SetHiddenInUI()
                .SetGroup(bleeding_wound_group)
                .Configure();

            var constitution_bleed_ability = ActivatableAbilityConfigurator.New(CBleedAbility, CBleedAbilityGuid)
                .SetDisplayName(CBleedDebuffDisplayName)
                .SetDescription(CBleedAbilityDescription)
                .SetIcon(BuffRefs.ConstitutionDamage.Reference.Get().Icon)
                .SetBuff(constitution_bleed_buff)
                .SetDeactivateImmediately()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .SetHiddenInUI()
                .SetGroup(bleeding_wound_group)
                .Configure();

            var bleeding_wound_ability = ActivatableAbilityConfigurator.New(BWoundAbility, BWoundAbilityGuid)
                .SetDisplayName(BWoundDisplayName)
                .SetDescription(BWoundDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistCripplingCriticalBleedAbility.Reference.Get().Icon)
                .AddActivatableAbilityVariants(variants: new() { bleed_ability, strength_bleed_ability, dexterity_bleed_ability, constitution_bleed_ability })
                .AddActivationDisable()
                .Configure();

            return FeatureConfigurator.New(BWound, BWoundGuid)
                .SetDisplayName(BWoundDisplayName)
                .SetDescription(BWoundDescription)
                .SetIcon(ActivatableAbilityRefs.DuelistCripplingCriticalBleedAbility.Reference.Get().Icon)
                .AddFacts(new() { bleeding_wound_ability, bleed_ability, strength_bleed_ability, dexterity_bleed_ability, constitution_bleed_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateEvasive()
        {
            evasive_feature = FeatureConfigurator.New(Evasive, EvasiveGuid)
                .SetDisplayName(EvasiveDisplayName)
                .SetDescription(EvasiveDescription)
                .SetIcon(FeatureRefs.Evasion.Reference.Get().Icon)
                .AddComponent<Evasive>()
                .SetIsClassFeature()
                .Configure();

            return evasive_feature;
        }

        internal static BlueprintFeature CreateSubtle()
        {
            subtle_feature = FeatureConfigurator.New(Subtle, SubtleGuid)
                .SetDisplayName(SubtleDisplayName)
                .SetDescription(SubtleDescription)
                .SetIcon(FeatureRefs.TwoHandedFighterStrongGrip.Reference.Get().Icon)
                .AddComponent<SubtleBlade>()
                .SetIsClassFeature()
                .Configure();

            return subtle_feature;
        }

        internal static BlueprintFeature CreateDeeds11()
        {
            return FeatureConfigurator.New(Deeds11, Deeds11Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateBleedingWound(), CreateEvasive(), CreateSubtle() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDizzying()
        {
            dizzying_defense_buff = BuffConfigurator.New(DizzyingBuff, DizzyingBuffGuid)
                .AddNotDispelable()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            var dizzying_ability = AbilityConfigurator.New(DizzyingAbility, DizzyingAbilityGuid)
                .SetDisplayName(DizzyingDisplayName)
                .SetDescription(DizzyingDescription)
                .SetIcon(AbilityRefs.MageShield.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetActionType(UnitCommand.CommandType.Swift)
                .SetRange(AbilityRange.Weapon)
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(buff: dizzying_defense_buff, durationValue: ContextDuration.Fixed(1), toCaster: true).ApplyBuff(buff: BuffRefs.FightDefensivelyBuff.Reference.Get(), durationValue: ContextDuration.Fixed(1), toCaster: true).MeleeAttack().Build())
                .Configure();

            return FeatureConfigurator.New(DizzyingFeature, DizzyingFeatureGuid)
                .SetDisplayName(DizzyingDisplayName)
                .SetDescription(DizzyingDescription)
                .SetIcon(AbilityRefs.MageShield.Reference.Get().Icon)
                .AddFacts(new() { dizzying_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreatePerfectThrust()
        {
            var perfect_thrust_buff = BuffConfigurator.New(PerfectThrustBuff, PerfectThrustBuffGuid)
                .AddComponent<IgnoreDamageReductionOnTarget>()
                .AddComponent<AttackTypeChange>(c => c.NewType = AttackType.Touch)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .Configure();

            var perfect_thrust_ability = AbilityConfigurator.New(PerfectThrustAbility, PerfectThrustAbilityGuid)
                .SetDisplayName(PerfectThrustDisplayName)
                .SetDescription(PerfectThrustDescription)
                .SetIcon(AbilityRefs.DimensionStrikeAbility.Reference.Get().Icon)
                .SetType(AbilityType.Extraordinary)
                .AllowTargeting(enemies: true)
                .SetIsFullRoundAction()
                .SetRange(AbilityRange.Weapon)
                .AddAbilityEffectRunAction(actions: ActionsBuilder.New()
                    .ApplyBuff(perfect_thrust_buff, ContextDuration.Fixed(1), toCaster: true)
                    .MeleeAttack()
                    .RemoveBuff(perfect_thrust_buff, toCaster: true).Build())
                .AddComponent<AttackAnimation>()
                .AddComponent<AbilityCasterSwashbucklerWeaponCheck>()
                .AddComponent<AbilityCasterHasAtLeastOnePanache>()
                .Configure();

            return FeatureConfigurator.New(PerfectThrustFeature, PerfectThrustFeatureGuid)
                .SetDisplayName(PerfectThrustDisplayName)
                .SetDescription(PerfectThrustDescription)
                .SetIcon(AbilityRefs.DimensionStrikeAbility.Reference.Get().Icon)
                .AddFacts(new() { perfect_thrust_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateEdge()
        {
            var edge_buff = BuffConfigurator.New(EdgeBuff, EdgeBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .AddComponent<SwashbucklerEdge>()
                .Configure();

            var edge_ability = ActivatableAbilityConfigurator.New(EdgeAbility, EdgeAbilityGuid)
                .SetDisplayName(EdgeDisplayName)
                .SetDescription(EdgeDescription)
                .SetIcon(FeatureRefs.BloodlineFeyWoodlandStride.Reference.Get().Icon)
                .SetBuff(edge_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(EdgeFeature, EdgeFeatureGuid)
                .SetDisplayName(EdgeDisplayName)
                .SetDescription(EdgeDescription)
                .SetIcon(FeatureRefs.BloodlineFeyWoodlandStride.Reference.Get().Icon)
                .AddFacts(new() { edge_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds15()
        {
            return FeatureConfigurator.New(Deeds15, Deeds15Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateDizzying(), CreatePerfectThrust(), CreateEdge() })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateCheatDeath()
        {
            var cheat_buff = BuffConfigurator.New(CheatDeathBuff, CheatDeathBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<CheatDeath>()
                .Configure();

            var cheat_ability = ActivatableAbilityConfigurator.New(CheatDeathAbility, CheatDeathAbilityGuid)
                .SetDisplayName(CheatDeathDisplayName)
                .SetDescription(CheatDeathDescription)
                .SetIcon(AbilityRefs.ShadowConjurationGreater.Reference.Get().Icon)
                .SetBuff(cheat_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(CheatDeathFeature, CheatDeathFeatureGuid)
                .SetDisplayName(CheatDeathDisplayName)
                .SetDescription(CheatDeathDescription)
                .SetIcon(AbilityRefs.ShadowConjurationGreater.Reference.Get().Icon)
                .AddFacts(new() { cheat_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeadly()
        {
            var deadly_buff = BuffConfigurator.New(DeadlyBuff, DeadlyBuffGuid)
                .SetDisplayName(DeadlyDisplayName)
                .SetDescription(DeadlyDescription)
                .AddNotDispelable()
                .AddContextCalculateAbilityParamsBasedOnClass(swash_class, statType: StatType.Dexterity)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, criticalHit: true, action: ActionsBuilder.New().SavingThrow(SavingThrowType.Fortitude).ApplyBuff(BuffRefs.MasterStrikeDamageBuff.Reference.Get(), ContextDuration.Fixed(1)))
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, criticalHit: true, action: ActionsBuilder.New().ContextSpendResource(panache_resource, ContextValues.Constant(1)))
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();

            var deadly_ability = ActivatableAbilityConfigurator.New(DeadlyAbility, DeadlyAbilityGuid)
                .SetDisplayName(DeadlyDisplayName)
                .SetDescription(DeadlyDescription)
                .SetIcon(FeatureRefs.TwoHandedFighterDevastatingBlowFeature.Reference.Get().Icon)
                .SetBuff(deadly_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(DeadlyFeature, DeadlyFeatureGuid)
                .SetDisplayName(DeadlyDisplayName)
                .SetDescription(DeadlyDescription)
                .SetIcon(FeatureRefs.TwoHandedFighterDevastatingBlowFeature.Reference.Get().Icon)
                .AddFacts(new() { deadly_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateStunning()
        {
            var stunning_buff = BuffConfigurator.New(StunningBuff, StunningBuffGuid)
                .SetDisplayName(StunningDisplayName)
                .SetDescription(StunningDescription)
                .AddNotDispelable()
                .AddContextCalculateAbilityParamsBasedOnClass(swash_class, statType: StatType.Dexterity)
                .AddInitiatorAttackWithWeaponTrigger(duelistWeapon: true, action: ActionsBuilder.New().SavingThrow(SavingThrowType.Fortitude).ApplyBuff(BuffRefs.Stunned.Reference.Get(), ContextDuration.Fixed(1)).ContextSpendResource(panache_resource, ContextValues.Constant(2)))
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 2; })
                .Configure();

            var stunning_ability = ActivatableAbilityConfigurator.New(StunningAbility, StunningAbilityGuid)
                .SetDisplayName(StunningDisplayName)
                .SetDescription(StunningDescription)
                .SetIcon(FeatureRefs.MasterStrike.Reference.Get().Icon)
                .SetBuff(stunning_buff)
                .SetDeactivateImmediately()
                .Configure();

            return FeatureConfigurator.New(StunningFeature, StunningFeatureGuid)
                .SetDisplayName(StunningDisplayName)
                .SetDescription(StunningDescription)
                .SetIcon(FeatureRefs.MasterStrike.Reference.Get().Icon)
                .AddFacts(new() { stunning_ability })
                .SetIsClassFeature()
                .Configure();
        }

        internal static BlueprintFeature CreateDeeds19()
        {
            return FeatureConfigurator.New(Deeds19, Deeds19Guid)
                .SetDisplayName(DeedsDisplayName)
                .SetDescription(DeedsDescription)
                .AddFacts(new() { CreateCheatDeath(), CreateDeadly(), CreateStunning() })
                .SetIsClassFeature()
                .Configure();
        }
    }
}