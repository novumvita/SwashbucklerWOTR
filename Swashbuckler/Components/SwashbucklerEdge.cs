using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;

namespace Swashbuckler.Components
{
    internal class SwashbucklerEdge : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSkillCheck>, IInitiatorRulebookSubscriber
    {
        private StatType[] stats = { StatType.SkillAthletics, StatType.SkillMobility };

        public void OnEventAboutToTrigger(RuleSkillCheck evt)
        {
            if (!stats.Contains(evt.StatType))
                return;
            evt.Take10ForSuccess = true;
        }

        public void OnEventDidTrigger(RuleSkillCheck evt)
        {
        }
    }
}
