using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System.Linq;

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
    internal class WarriorPoetEdge : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSkillCheck>, IInitiatorRulebookSubscriber
    {
        private StatType[] stats = { StatType.SkillAthletics, StatType.SkillMobility, StatType.SkillPersuasion };

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
