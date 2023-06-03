using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Feats
{
    internal class Mythic
    {
        internal const string AbundantPanacheName = "AbundantPanacheFeat";
        internal const string AbundantPanacheGuid = "BD2A0AC5-AB53-4A3F-BE66-8460C496E5DA";
        internal const string AbundantPanacheDisplayName = "AbundantPanache.Name";
        internal const string AbundantPanacheDescription = "AbundantPanache.Description";
        internal static void Configure()
        {
            var abundant_panache = FeatureConfigurator.New(AbundantPanacheName, AbundantPanacheGuid, FeatureGroup.MythicAbility)
                .SetDisplayName(AbundantPanacheDisplayName)
                .SetDescription(AbundantPanacheDescription)
                .SetIcon(FeatureRefs.Bravery.Reference.Get().Icon)
                .AddInitiatorAttackWithWeaponTrigger(action: ActionsBuilder.New().RestoreResource(Swashbuckler.panache_resource, 1), actionsOnInitiator: true, duelistWeapon: true, onlyHit: true)
                .Configure();
        }
    }
}
