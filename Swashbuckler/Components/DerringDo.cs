using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    internal class DerringDo : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSkillCheck>, IInitiatorRulebookSubscriber
    {
        private StatType[] stats = { StatType.SkillAthletics, StatType.SkillMobility };
        private bool willSpend = false;

        public int CalculateExplodingDice(RuleSkillCheck evt)
        {
            RuleRollDice rule = new RuleRollDice(evt.Initiator, new DiceFormula(1, DiceType.D6));

            int roll = this.Fact.MaybeContext.TriggerRule<RuleRollDice>(rule).Result;

            int total = roll;

            if (roll == 6)
            {
                int attempts = Owner.Stats.Dexterity.Bonus > 0 ? Owner.Stats.Dexterity.Bonus : 1;

                for (int x = 0; x < attempts; x++)
                {
                    roll = this.Fact.MaybeContext.TriggerRule<RuleRollDice>(rule).Result;
                    total += roll;
                    if (roll != 6)
                        break;
                }
            }

            return total;
            
        }

        public void OnEventAboutToTrigger(RuleSkillCheck evt)
        {
            if (!stats.Contains(evt.StatType))
                return;

            evt.Bonus.AddModifier(CalculateExplodingDice(evt), base.Fact, ModifierDescriptor.UntypedStackable);
            willSpend = true;
            
        }

        public void OnEventDidTrigger(RuleSkillCheck evt)
        {
            if (willSpend)
            {
                Owner.Descriptor.Resources.Spend(Swashbuckler.panache_resource, 1);
                willSpend= false;
            }
        }
    }
}
