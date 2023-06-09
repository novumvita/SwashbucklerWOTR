using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using CharacterOptionsPlus.MechanicsChanges;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using Swashbuckler.Utilities;
using UnityEngine;
using static Kingmaker.RuleSystem.RulebookEvent;

namespace Swashbuckler.Feats
{
    internal class SeizeTheOpportunity
    {
        internal static BlueprintBuff StOBuff;

        private static string StOBuffName = "SeizeTheOpportunityBuff";
        private static string StOBuffGuid = "1BF9A0CC-A92A-44B7-B9AE-9AB0EA708791";

        private static string StOAbilityName = "SeizeTheOpportunityAbility";
        private static string StOAbilityGuid = "439E68E3-E797-488D-91B0-0736882F9318";

        private static string StOFeatName = "SeizeTheOpportunityFeature";
        private static string StOFeatGuid = "63CC35E0-71C4-4CD3-92EF-2DCB8B5855CF";
        private static string StOFeatDisplayName = "SeizeTheOpportunity.Name";
        private static string StOFeatDescription = "SeizeTheOpportunity.Description";

        internal static void Configure()
        {
            StOBuff = BuffConfigurator.New(StOBuffName, StOBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddComponent<SeizeTheOpportunityComponent>()
                .AddNotDispelable()
                .Configure();

            var StOAbility = ActivatableAbilityConfigurator.New(StOAbilityName, StOAbilityGuid)
                .SetDisplayName(StOFeatDisplayName)
                .SetDescription(StOFeatDescription)
                .SetIcon(FeatureRefs.VitalStrikeFeature.Reference.Get().Icon)
                .SetBuff(StOBuff)
                .SetDeactivateImmediately()
                .Configure();

            FeatureConfigurator.New(StOFeatName, StOFeatGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(StOFeatDisplayName)
                .SetDescription(StOFeatDescription)
                .SetIcon(FeatureRefs.VitalStrikeFeature.Reference.Get().Icon)
                .AddFacts(new() { StOAbility })
                .AddPrerequisiteFeature(FeatureRefs.CombatReflexes.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.VitalStrikeFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 8)
                .Configure();
        }
    }

    internal class SeizeTheOpportunityComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(VitalStrikeEventHandler));

        private static readonly CustomDataKey HandlerKey = new("SeizeTheOpportunity.Handler");

        private static BlueprintFeature _vitalStrike;
        private static BlueprintFeature VitalStrike
        {
            get
            {
                _vitalStrike ??= FeatureRefs.VitalStrikeFeature.Reference.Get();
                return _vitalStrike;
            }
        }

        private static BlueprintFeature _vitalStrikeImproved;
        private static BlueprintFeature VitalStrikeImproved
        {
            get
            {
                _vitalStrikeImproved ??= FeatureRefs.VitalStrikeFeatureImproved.Reference.Get();
                return _vitalStrikeImproved;
            }
        }

        private static BlueprintFeature _vitalStrikeGreater;
        private static BlueprintFeature VitalStrikeGreater
        {
            get
            {
                _vitalStrikeGreater ??= FeatureRefs.VitalStrikeFeatureGreater.Reference.Get();
                return _vitalStrikeGreater;
            }
        }

        private static BlueprintFeature _vitalStrikeMythic;
        private static BlueprintFeature VitalStrikeMythic
        {
            get
            {
                _vitalStrikeMythic ??= FeatureRefs.VitalStrikeMythicFeat.Reference.Get();
                return _vitalStrikeMythic;
            }
        }

        private static BlueprintFeature _rowdy;
        private static BlueprintFeature Rowdy
        {
            get
            {
                _rowdy ??= FeatureRefs.RowdyVitalDamage.Reference.Get();
                return _rowdy;
            }
        }

        private void RegisterVitalStrike(RuleAttackWithWeapon evt)
        {
            Logger.Verbose(() => "Adding Vital Strike handler");
            var vitalStrikeMod = Owner.HasFact(VitalStrikeGreater) ? 4 : Owner.HasFact(VitalStrikeImproved) ? 3 : 2;
            var handler =
              new VitalStrikeEventHandler(
                Owner, vitalStrikeMod, Owner.HasFact(VitalStrikeMythic), Owner.HasFact(Rowdy), Fact);
            EventBus.Subscribe(handler);
            evt.SetCustomData(HandlerKey, handler);
        }

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (!evt.IsAttackOfOpportunity)
                return;

            RegisterVitalStrike(evt);
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
        }
    }
}
