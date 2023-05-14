using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Swashbuckler.Archetypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class InspiredStrike : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IInitiatorRulebookSubscriber
    {
        private int to_spend = 0;
        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.Weapon.Blueprint.Category != WeaponCategory.Rapier)
                return;

            var bonus = evt.Initiator.Stats.Intelligence.Bonus < 1 ? 1 : evt.Initiator.Stats.Intelligence.Bonus;

            evt.AddTemporaryModifier(evt.Initiator.Stats.AdditionalAttackBonus.AddModifier(bonus, ModifierDescriptor.Insight));

            to_spend = 1;

            if (evt.Initiator.Descriptor.HasFact(InspiredBlade.inspired_crit_buff) && evt.Initiator.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) > 1)
            {
                to_spend = 2;
                evt.AutoCriticalThreat = true;
            }
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (to_spend > 0 && evt.AttackRoll.IsHit)
                evt.Initiator.Descriptor.Resources.Spend(Swashbuckler.panache_resource, to_spend);

            to_spend = 0;
        }
    }
}
