using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.TurnBasedMode;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Pathfinding;
using Swashbuckler.Components;
using Swashbuckler.Utilities;
using TurnBased.Controllers;
using UnityEngine;

namespace Swashbuckler.Feats
{
    internal class SpringAttack
    {
        #region const strings
        private const string SpringAttackFeat = "SpringAttackFeat";
        private const string SpringAttackFeatGuid = "9D46135E-3DC2-44B8-ABFD-45CA33805FF0";
        private const string SpringAttackFeatDisplayName = "SpringAttackFeat.Name";
        private const string SpringAttackFeatDescription = "SpringAttackFeat.Description";

        private const string SpringAttack1Feat = "SpringAttackImproved";
        private const string SpringAttack1FeatGuid = "15EB42AB-ABFF-46B4-9F48-DC615CCF0315";
        private const string SpringAttack1FeatDisplayName = "SpringAttackImproved.Name";
        private const string SpringAttack1FeatDescription = "SpringAttackImproved.Description";

        private const string SpringAttack2Feat = "SpringAttackGreater";
        private const string SpringAttack2FeatGuid = "3B81B854-07AC-4085-8ACA-C15854911832";
        private const string SpringAttack2FeatDisplayName = "SpringAttackGreater.Name";
        private const string SpringAttack2FeatDescription = "SpringAttackGreater.Description";

        private const string SpringAttackReapFeat = "SpringAttackReaping";
        private const string SpringAttackReapFeatGuid = "2AC1C633-0EC2-413F-AF5C-3F3F4C3F7A8A";
        private const string SpringAttackReapAbility = "SpringAttackReapingAbility";
        private const string SpringAttackReapAbilityGuid = "56F712F6-8353-48A7-BC68-3B9CFD2E4170";
        private const string SpringAttackReapBuff = "SpringAttackReapingBuff";
        private const string SpringAttackReapBuffGuid = "B0CD2014-A55C-4FBB-9BFB-FE9CEC897C86";
        private const string SpringAttackReapFeatDisplayName = "SpringAttackReaping.Name";
        private const string SpringAttackReapFeatDescription = "SpringAttackReaping.Description";

        private const string SpringAttackBuffDisplayName = "SpringAttackBuff.Name";

        private const string SpringAttackBuff = "SpringAttackBuff";
        private const string SpringAttackBuffGuid = "4A1F9D59-AA45-4251-9F7B-7017EF851455";
        private const string SpringAttackBuffDescription = "SpringAttackBuff.Description";

        private const string SpringAttackBuff2 = "SpringAttackBuff2";
        private const string SpringAttackBuff2Guid = "D651E52B-5DF7-4D7A-828F-684E886D660E";
        private const string SpringAttackBuff2Description = "SpringAttackBuff2.Description";

        private const string SpringAttackBuff3 = "SpringAttackBuff3";
        private const string SpringAttackBuff3Guid = "57880A35-4093-498F-B80F-9FE3A7757A51";
        private const string SpringAttackBuff3Description = "SpringAttackBuff3.Description";

        private const string SpringAttackDebuff2 = "SpringAttackDebuff2";
        private const string SpringAttackDebuff2Guid = "62F76118-2D90-4570-A82C-A019A26E7A74";

        private const string SpringAttackDebuff3 = "SpringAttackDebuff3";
        private const string SpringAttackDebuff3Guid = "66D4B0F1-F71B-428D-8E73-46F697265CE4";
        #endregion

        internal static BlueprintBuff springAttackBuff1;
        internal static BlueprintBuff springAttackBuff2;
        internal static BlueprintBuff springAttackBuff3;
        internal static BlueprintBuff springAttackReapingBuff;

        internal static BlueprintBuff springAttackDebuff2;
        internal static BlueprintBuff springAttackDebuff3;

        internal static BlueprintFeature springAttackFeat;
        internal static BlueprintFeature springAttackReaping;
        internal static BlueprintFeature springAttack1;
        internal static BlueprintFeature springAttack2;

        internal const string KitsuneName = "DancersKitsuneBuff";
        internal const string KitsuneGuid = "C3D3E659-EC64-4CBF-9790-E7FAE49C851C";

        static internal BlueprintBuff kitsune;
        internal static BlueprintBuff CreateSpringAttackBuff1()
        {
            return BuffConfigurator.New(SpringAttackBuff, SpringAttackBuffGuid)
                .SetDisplayName(SpringAttackBuffDisplayName)
                .SetDescription(SpringAttackBuffDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();
        }
        internal static BlueprintBuff CreateSpringAttackBuff2()
        {
            return BuffConfigurator.New(SpringAttackBuff2, SpringAttackBuff2Guid)
                .SetDisplayName(SpringAttackBuffDisplayName)
                .SetDescription(SpringAttackBuff2Description)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();
        }
        internal static BlueprintBuff CreateSpringAttackBuff3()
        {
            return BuffConfigurator.New(SpringAttackBuff3, SpringAttackBuff3Guid)
                .SetDisplayName(SpringAttackBuffDisplayName)
                .SetDescription(SpringAttackBuff3Description)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddNotDispelable()
                .Configure();
        }

        internal static BlueprintBuff CreateKitsune()
        {
            return BuffConfigurator.New(SpringAttack.KitsuneName, SpringAttack.KitsuneGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .AddComponent<AddAbilityResourceDepletedTrigger>(c => { c.m_Resource = Swashbuckler.panache_resource.ToReference<BlueprintAbilityResourceReference>(); c.Action = ActionsBuilder.New().RemoveSelf().Build(); c.Cost = 1; })
                .Configure();
        }
        internal static void CreateSpringAttackFeat()
        {
            springAttackDebuff2 = BuffConfigurator.New(SpringAttackDebuff2, SpringAttackDebuff2Guid)
                .AddAttackBonus(-5)
                .AddRemoveBuffOnAttack()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .Configure();

            springAttackDebuff3 = BuffConfigurator.New(SpringAttackDebuff3, SpringAttackDebuff3Guid)
                .AddAttackBonus(-10)
                .AddRemoveBuffOnAttack()
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .Configure();

            springAttackBuff1 = CreateSpringAttackBuff1();
            springAttackBuff2 = CreateSpringAttackBuff2();
            springAttackBuff3 = CreateSpringAttackBuff3();

            kitsune = CreateKitsune();

            springAttackFeat = FeatureConfigurator.New(SpringAttackFeat, SpringAttackFeatGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SpringAttackFeatDisplayName)
                .SetDescription(SpringAttackFeatDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddComponent<SpringAttackController>()
                .AddPrerequisiteStatValue(StatType.Dexterity, 13)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.Mobility.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 4)
                .Configure();

            springAttack1 = FeatureConfigurator.New(SpringAttack1Feat, SpringAttack1FeatGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SpringAttack1FeatDisplayName)
                .SetDescription(SpringAttack1FeatDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddPrerequisiteStatValue(StatType.Dexterity, 15)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteFeature(springAttackFeat)
                .AddPrerequisiteFeature(FeatureRefs.Mobility.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 9)
                .Configure();

            springAttack2 = FeatureConfigurator.New(SpringAttack2Feat, SpringAttack2FeatGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SpringAttack2FeatDisplayName)
                .SetDescription(SpringAttack2FeatDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddPrerequisiteStatValue(StatType.Dexterity, 17)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteFeature(springAttackFeat)
                .AddPrerequisiteFeature(springAttack1)
                .AddPrerequisiteFeature(FeatureRefs.Mobility.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 16)
                .Configure();

            springAttackReapingBuff = BuffConfigurator.New(SpringAttackReapBuff, SpringAttackReapBuffGuid)
                .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                .AddNotDispelable()
                .Configure();

            var reapingAbility = ActivatableAbilityConfigurator.New(SpringAttackReapAbility, SpringAttackReapAbilityGuid)
                .SetDisplayName(SpringAttackReapFeatDisplayName)
                .SetDescription(SpringAttackReapFeatDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .SetBuff(springAttackReapingBuff)
                .SetDeactivateImmediately()
                .Configure();

            springAttackReaping = FeatureConfigurator.New(SpringAttackReapFeat, SpringAttackReapFeatGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(SpringAttackReapFeatDisplayName)
                .SetDescription(SpringAttackReapFeatDescription)
                .SetIcon(FeatureRefs.Improved_Initiative.Reference.Get().Icon)
                .AddFacts(new() { reapingAbility })
                .AddPrerequisiteStatValue(StatType.Dexterity, 15)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteFeature(springAttackFeat)
                .AddPrerequisiteFeature(FeatureRefs.VitalStrikeFeature.Reference.Get())
                .AddPrerequisiteFeature(FeatureRefs.Mobility.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 11)
                .Configure();
        }
    }
    internal class SpringAttackController : UnitFactComponentDelegate, IUnitCommandStartHandler, IUnitCommandEndHandler, IUnitRunCommandHandler, IUnitSubscriber
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("SpringAttack");

        private static UnitEntityData target1;
        private static UnitEntityData target2;
        private static UnitEntityData target3;

        public void HandleUnitCommandDidEnd(UnitCommand command)
        {
            UnitAttack unitAttack = command as UnitAttack;

            UnitUseAbility unitUseAbility = command as UnitUseAbility;
            AbilityCustomVitalStrike vitalStrikeAbility = unitUseAbility?.Ability.Blueprint.GetComponent<AbilityCustomVitalStrike>();

            if (!(unitAttack != null || (command.Executor.GetFact(SpringAttack.springAttackReapingBuff) != null && vitalStrikeAbility != null)))
            {
                Logger.Log("No disengagement");
                return;
            }

            if (command.Executor.HasFact(SpringAttack.springAttackBuff1) && target1 != null)
            {
                Logger.Log("Disengaged from first target");
                target1.CombatState.m_EngagedBy.Remove(command.Executor);
                target1.CombatState.m_EngagedUnits.Remove(command.Executor);
                return;
            }
            if (command.Executor.HasFact(SpringAttack.springAttackBuff2) && target2 != null)
            {
                Logger.Log("Disengaged from second target");
                target2.CombatState.m_EngagedBy.Remove(command.Executor);
                target2.CombatState.m_EngagedUnits.Remove(command.Executor);
                return;
            }
            if (command.Executor.HasFact(SpringAttack.springAttackBuff3) && target3 != null)
            {
                Logger.Log("Disengaged from third target");
                target3.CombatState.m_EngagedBy.Remove(command.Executor);
                target3.CombatState.m_EngagedUnits.Remove(command.Executor);
                return;
            }
        }

        public void HandleUnitCommandDidStart(UnitCommand command)
        {
            if (!CombatController.IsInTurnBasedCombat())
            {
                return;
            }

            UnitUseAbility unitUseAbility = command as UnitUseAbility;
            AbilityCustomVitalStrike vitalStrikeAbility = unitUseAbility?.Ability.Blueprint.GetComponent<AbilityCustomVitalStrike>();

            UnitAttack unitAttack = command as UnitAttack;

            if (!(unitAttack != null || (command.Executor.GetFact(SpringAttack.springAttackReapingBuff) != null && vitalStrikeAbility != null)))
            {
                Logger.Log("Not attack and not vital strike + reaping");
                return;
            }

            Path path = PathVisualizer.Instance.m_CurrentPath;
            if (path == null)
            {
                Logger.Log("path == null");
                return;
            }

            if (!command.Executor.HasFact(SpringAttack.springAttackBuff1) && !command.Executor.HasFact(SpringAttack.springAttackBuff2) && !command.Executor.HasFact(SpringAttack.springAttackBuff3) && path.GetTotalLength() < 2f)
            {
                Logger.Log("Initial path too short");
                return;
            }

            if (!command.Executor.HasFact(SpringAttack.springAttackBuff1) && !command.Executor.HasFact(SpringAttack.springAttackBuff2) && !command.Executor.HasFact(SpringAttack.springAttackBuff3) && path.GetTotalLength() > 2f)
            {
                if (command.Executor.HasFact(SpringAttack.kitsune))
                    Logger.Log("First attack command");
                {
                    ;
                    Logger.Log("Attempting to feint");
                    Fact.RunActionInContext(ActionsBuilder.New().Add<ContextFeintSkillCheck>(c => c.Success = FeintFeats.feint_action).Build(), command.Target);
                    command.Executor.Resources.Spend(Swashbuckler.panache_resource, 1);
                }

                Logger.Log("First attack command");
                target1 = command.Target.Unit;
                command.Executor.AddBuff(SpringAttack.springAttackBuff1, command.Executor, 1.Rounds().Seconds);
                Logger.Log("Applied buff1");
                return;
            }

            if (command.Executor.HasFact(SpringAttack.springAttack1) && command.Executor.HasFact(SpringAttack.springAttackBuff1) && command.Target.Unit != null && command.Target.Unit != target1)
            {
                Logger.Log("Second attack command");
                target2 = command.Target.Unit;
                command.Executor.RemoveFact(SpringAttack.springAttackBuff1);
                Logger.Log("Removed buff1");
                command.Executor.AddBuff(SpringAttack.springAttackBuff2, command.Executor, 1.Rounds().Seconds);
                command.Executor.AddBuff(SpringAttack.springAttackDebuff2, command.Executor, 1.Rounds().Seconds);
                Logger.Log("Applied buff2");
                return;
            }

            if (command.Executor.HasFact(SpringAttack.springAttack2) && command.Executor.HasFact(SpringAttack.springAttackBuff2) && command.Target.Unit != null && command.Target.Unit != target1 && command.Target.Unit != target2)
            {
                Logger.Log("Third attack command");
                target3 = command.Target.Unit;
                command.Executor.RemoveFact(SpringAttack.springAttackBuff2);
                Logger.Log("Removed buff2");
                command.Executor.AddBuff(SpringAttack.springAttackBuff3, command.Executor, 1.Rounds().Seconds);
                command.Executor.AddBuff(SpringAttack.springAttackDebuff3, command.Executor, 1.Rounds().Seconds);
                Logger.Log("Applied buff3");
                return;
            }
        }

        public void HandleUnitRunCommand(UnitCommand command)
        {
            if (command.Executor.HasFact(SpringAttack.springAttack1) && command.Executor.HasFact(SpringAttack.springAttackBuff1) && command.Target.Unit != null && command.Target.Unit == target1)
            {
                command.Executor.CombatState.Cooldown.StandardAction = 6f;
                Logger.Log("Set standard cd for second attack");
                return;
            }
            if (command.Executor.HasFact(SpringAttack.springAttack2) && command.Executor.HasFact(SpringAttack.springAttackBuff2) && command.Target.Unit != null && (command.Target.Unit == target1 || command.Target.Unit == target2))
            {
                command.Executor.CombatState.Cooldown.StandardAction = 6f;
                Logger.Log("Set standard cd for third attack");
                return;
            }
            if (command.Executor.HasFact(SpringAttack.springAttack1) && command.Executor.HasFact(SpringAttack.springAttackBuff1) && command.Target.Unit != null && command.Target.Unit != target1)
            {
                command.Executor.CombatState.Cooldown.StandardAction = 0f;
                Logger.Log("Reset standard cd for second attack");
                return;
            }
            if (command.Executor.HasFact(SpringAttack.springAttack2) && command.Executor.HasFact(SpringAttack.springAttackBuff2) && command.Target.Unit != null && command.Target.Unit != target1)
            {
                command.Executor.CombatState.Cooldown.StandardAction = 0f;
                Logger.Log("Reset standard cd for third attack");
                return;
            }
        }
    }
}
