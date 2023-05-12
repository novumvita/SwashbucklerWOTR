using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Visual.Particles;
using Kingmaker.Visual.Particles.FxSpawnSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class CheatDeath : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Target.Descriptor.HPLeft > 0)
                return;
            if (evt.Target.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;
            evt.Target.Descriptor.Resources.Spend(Swashbuckler.panache_resource, evt.Target.Resources.GetResourceAmount(Swashbuckler.panache_resource));
            evt.Target.Damage += evt.Target.Descriptor.HPLeft - 1;
            FxHelper.SpawnFxOnUnit(AbilityRefs.Resurrection.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Target.View);
        }
    }
}
