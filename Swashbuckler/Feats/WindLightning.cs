using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Feats
{
    internal class WindLightning
    {
        private const string WindName = "WindStance";
        private const string WindGuid = "94163314-573F-4D03-BA6F-446FD55E3AE9";
        private const string WindBuffName = "WindStanceBuff";
        private const string WindBuffGuid = "892449A6-653E-4BA4-9C89-40077F9518EE";
        private const string WindDisplayName = "WindStance.Name";
        private const string WindDescription = "WindStance.Description";

        private const string LightningName = "LightningStance";
        private const string LightningGuid = "03E9FDC3-0584-4BAB-A850-A9CECF87FFFD";
        private const string LightningBuffName = "LightningStanceBuff";
        private const string LightningBuffGuid = "9087E4C3-8C73-497C-8991-0966BDD1D968";
        private const string LightningDisplayName = "LightningStance.Name";
        private const string LightningDescription = "LightningStance.Description";

        internal static void Configure()
        {
            var lightning_buff = BuffConfigurator.New(LightningBuffName, LightningBuffGuid)
                .SetDisplayName(LightningDisplayName)
                .SetDescription(LightningDescription)
                .SetIcon(BuffRefs.BlurBuff.Reference.Get().Icon)
                .AddSetAttackerMissChance(type: SetAttackerMissChance.Type.All, value: 50)
                .SetFxOnStart(BuffRefs.BlurBuff.Reference.Get().FxOnStart)
                .SetFxOnRemove(BuffRefs.BlurBuff.Reference.Get().FxOnRemove)
                .Configure();

            var wind_buff = BuffConfigurator.New(WindBuffName, WindBuffGuid)
                .SetDisplayName(WindDisplayName)
                .SetDescription(WindDescription)
                .AddSetAttackerMissChance(type: SetAttackerMissChance.Type.Ranged, value: 20)
                .SetIcon(BuffRefs.BlurBuff.Reference.Get().Icon)
                .Configure();

            var lightning_stance = FeatureConfigurator.New(LightningName, LightningGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .Configure();

            var wind_stance = FeatureConfigurator.New(WindName, WindGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetDisplayName(WindDisplayName)
                .SetDescription(WindDescription)
                .SetIcon(FeatureRefs.LightningReflexes.Reference.Get().Icon)
                .AddMovementDistanceTrigger(action: ActionsBuilder.New().Conditional(ConditionsBuilder.New().CasterHasFact(lightning_stance), ifFalse: ActionsBuilder.New().ApplyBuff(wind_buff, ContextDuration.Fixed(1), toCaster: true)), distanceInFeet: 10, limitTiggerCountInOneRound: true, tiggerCountMaximumInOneRound: 1)
                .AddPrerequisiteStatValue(StatType.Dexterity, 15)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 6)
                .Configure();

            lightning_stance = FeatureConfigurator.For(lightning_stance)
                .SetDisplayName(LightningDisplayName)
                .SetDescription(LightningDescription)
                .SetIcon(FeatureRefs.LightningReflexes.Reference.Get().Icon)
                .AddMovementDistanceTrigger(action: ActionsBuilder.New().ApplyBuff(lightning_buff, ContextDuration.Fixed(1), toCaster: true).RemoveBuff(wind_buff), distanceInFeet: 60, limitTiggerCountInOneRound: true, tiggerCountMaximumInOneRound: 1)
                .AddPrerequisiteStatValue(StatType.Dexterity, 17)
                .AddPrerequisiteFeature(FeatureRefs.Dodge.Reference.Get())
                .AddPrerequisiteFeature(wind_stance)
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 11)
                .Configure();
        }
    }
}
