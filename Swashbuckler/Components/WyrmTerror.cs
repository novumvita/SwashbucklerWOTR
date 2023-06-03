using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.References;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class WyrmTerror : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IInitiatorRulebookSubscriber
    {
        private bool added_fact = false;
        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (!evt.IsFullAttack || !evt.IsFirstAttack)
                return;
            if (evt.Initiator.GetFact(FeatureRefs.ThugFrightening.Reference.Get()) == null)
            {
                added_fact = true;
                evt.Initiator.AddFact(FeatureRefs.ThugFrightening.Reference.Get());
            }

            Fact.RunActionInContext(ActionsBuilder.New().CastSpell(AbilityRefs.DazzlingDisplayAction.Reference.Get()).Build());

            if (added_fact)
                evt.Initiator.RemoveFact(FeatureRefs.ThugFrightening.Reference.Get());
            added_fact = false;
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
        }
    }
}
