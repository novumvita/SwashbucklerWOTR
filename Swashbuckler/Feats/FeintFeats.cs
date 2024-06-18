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
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Swashbuckler.Components;
using TabletopTweaks.Core.Utilities;

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

        internal const string FeintName = "FeintFeat";
        internal const string FeintGuid = "15C8D785-22B8-4602-AB59-B943E88E3EB8";
        internal const string FeintDisplayName = "Feint.Name";
        internal const string FeintDescription = "Feint.Description";

        internal const string FeintAbilityName = "FeintAbility";
        internal const string FeintAbilityGuid = "78B4CE1D-5360-4B8B-8B97-0F199D28A5E3";

        internal const string GFeintName = "GFeintFeat";
        internal const string GFeintGuid = "33E0B750-265E-408E-A5A2-8FEF5E029CF0";
        internal const string GFeintDisplayName = "GFeint.Name";
        internal const string GFeintDescription = "GFeint.Description";

        internal const string FeintDebuffName = "FeintDebuff";
        internal const string FeintDebuffGuid = "7F514D75-5843-4268-96B2-9795EDEF6E01";

        internal const string GFeintDebuffName = "GFeintDebuff";
        internal const string GFeintDebuffGuid = "41504AAA-AEC2-4DF9-A307-6CB470F9242F";

        internal static void Configure()
        {
            if (DLCTools.HasDLC(6))
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

            else
            {
                var feint_ability = AbilityConfigurator.New(FeintAbilityName, FeintAbilityGuid)
                    .SetDisplayName(FeintDisplayName)
                    .SetDescription(FeintDescription)
                    .SetIcon(ActivatableAbilityRefs.LungeToggleAbility.Reference.Get().Icon)
                    .SetType(AbilityType.CombatManeuver)
                    .AllowTargeting(enemies: true)
                    .SetActionType(UnitCommand.CommandType.Move)
                    .SetRange(AbilityRange.Weapon)
                    .AddComponent<AttackAnimation>()
                    .Configure();

                feint_feat = FeatureConfigurator.New(FeintName, FeintGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .SetDisplayName(FeintDisplayName)
                    .SetDescription(FeintDescription)
                    .SetIcon(ActivatableAbilityRefs.LungeToggleAbility.Reference.Get().Icon)
                    .AddFacts(new() { feint_ability })
                    .AddPrerequisiteStatValue(StatType.Intelligence, 13)
                    .AddPrerequisiteFeature(FeatureRefs.CombatExpertiseFeature.Reference.Get())
                    .Configure();

                greater_feint_feat = FeatureConfigurator.New(GFeintGuid, GFeintGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .SetDisplayName(GFeintDisplayName)
                    .SetDescription(GFeintDescription)
                    .SetIcon(ActivatableAbilityRefs.LungeToggleAbility.Reference.Get().Icon)
                    .AddPrerequisiteFeature(feint_feat)
                    .AddPrerequisiteStatValue(StatType.Intelligence, 13)
                    .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 6)
                    .AddPrerequisiteFeature(FeatureRefs.CombatExpertiseFeature.Reference.Get())
                    .Configure();

                feint_buff = BuffConfigurator.New(FeintDebuffName, FeintDebuffGuid)
                    .SetDisplayName(Swashbuckler.FeintDebuffDisplayName)
                    .SetDescription(Swashbuckler.FeintDebuffDescription)
                    .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
                    .AddComponent<FeintFlatFooted>(c => { c.greater = false; })
                    .Configure();

                greater_feint_buff = BuffConfigurator.New(GFeintDebuffName, GFeintDebuffGuid)
                    .SetDisplayName(Swashbuckler.FeintDebuffDisplayName)
                    .SetDescription(Swashbuckler.FeintDebuffDescription)
                    .SetIcon(FeatureRefs.SneakAttack.Reference.Get().Icon)
                    .AddComponent<FeintFlatFooted>(c => { c.greater = true; })
                    .Configure();

                greater_feint_action = ActionsBuilder.New().ApplyBuff(greater_feint_buff, durationValue: ContextDuration.Fixed(1)).Build();

                feint_action = ActionsBuilder.New()
                    .Conditional(ConditionsBuilder.New().CasterHasFact(greater_feint_feat).Build(),
                    ifFalse: ActionsBuilder.New().ApplyBuff(feint_buff, durationValue: ContextDuration.Fixed(1)),
                    ifTrue: greater_feint_action)
                    .Build();

                vanilla_feint_action = ActionsBuilder.New().Add<ContextFeintSkillCheck>(c => c.Success = feint_action).Build();

                AbilityConfigurator.For(feint_ability)
                    .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ContextFeintSkillCheck>(c => c.Success = feint_action))
                    .Configure();
            }
        }
    }
}