using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace Swashbuckler.Feats
{
    internal class FeintFeats
    {
        static internal BlueprintFeature feint_feat;
        static internal BlueprintFeature greater_feint_feat;

        static internal BlueprintBuff feint_buff;
        static internal BlueprintBuff greater_feint_buff;

        static internal ActionList vanilla_feint_action;
        static internal ActionList feint_action;
        static internal ActionList greater_feint_action;

        internal const string FeintName = "FeintFeat - Removed";
        internal const string FeintGuid = "15C8D785-22B8-4602-AB59-B943E88E3EB8";

        internal const string FeintAbilityName = "FeintAbility - Removed";
        internal const string FeintAbilityGuid = "78B4CE1D-5360-4B8B-8B97-0F199D28A5E3";

        internal const string GFeintName = "GFeintFeat - Removed";
        internal const string GFeintGuid = "33E0B750-265E-408E-A5A2-8FEF5E029CF0";

        internal const string FeintDebuffName = "FeintDebuff - Removed";
        internal const string FeintDebuffGuid = "7F514D75-5843-4268-96B2-9795EDEF6E01";

        internal const string GFeintDebuffName = "GFeintDebuff - Removed";
        internal const string GFeintDebuffGuid = "41504AAA-AEC2-4DF9-A307-6CB470F9242F";

        internal static void Configure()
        {
            AbilityConfigurator.New(FeintAbilityName, FeintAbilityGuid)
                .Configure();

            FeatureConfigurator.New(FeintName, FeintGuid)
                .SetHideInUI()
                .Configure();

            feint_feat = FeatureRefs.Feint.Reference.Get();

            FeatureConfigurator.New(GFeintGuid, GFeintGuid)
                .SetHideInUI()
                .Configure();

            greater_feint_feat = FeatureRefs.FinalFeint.Reference.Get();

            BuffConfigurator.New(FeintDebuffName, FeintDebuffGuid)
                .Configure();

            feint_buff = BuffRefs.FeintBuffEnemy.Reference.Get();
            var feint_buff_ac = BuffRefs.FeintBuffEnemyAC.Reference.Get();

            BuffConfigurator.New(GFeintDebuffName, GFeintDebuffGuid)
                .Configure();

            greater_feint_buff = BuffRefs.FeintBuffEnemyFinalFeintEnemyBuff.Reference.Get();
            var greater_feint_buff_ac = BuffRefs.FeintBuffEnemyFinalFeintAC.Reference.Get();
            var greater_feint_buff_ally = BuffRefs.FeintBuffEnemyFinalFeinAllyBuff.Reference.Get();

            vanilla_feint_action = AbilityRefs.FeintAbility.Reference.Get().GetComponent<AbilityEffectRunAction>().Actions;

            greater_feint_action = ActionsBuilder.New().ApplyBuff(greater_feint_buff, durationValue: ContextDuration.Fixed(1)).ApplyBuff(greater_feint_buff_ac, durationValue: ContextDuration.Fixed(1)).PartyMembers(ActionsBuilder.New().ApplyBuff(greater_feint_buff_ally, durationValue: ContextDuration.Fixed(1))).Build();

            feint_action = ActionsBuilder.New()
                .Conditional(ConditionsBuilder.New().CasterHasFact(greater_feint_feat).Build(),
                ifFalse: ActionsBuilder.New().ApplyBuff(feint_buff, durationValue: ContextDuration.Fixed(1)).ApplyBuff(feint_buff_ac, durationValue: ContextDuration.Fixed(1)),
                ifTrue: greater_feint_action
                )
                .Build();
        }
    }
}
