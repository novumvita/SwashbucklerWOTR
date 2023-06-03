using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class DragonsRage : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttacksCount>, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
        public void OnEventAboutToTrigger(RuleCalculateAttacksCount evt)
        {
            if (evt.Initiator.Body.PrimaryHand.Weapon.Blueprint.Category != WeaponCategory.DuelingSword)
                return;
            evt.AddExtraAttacks(1, false, false);
        }

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.IsFullAttack && evt.IsFirstAttack)
                evt.Initiator.Resources.Spend(Swashbuckler.panache_resource, 1);

        }

        public void OnEventDidTrigger(RuleCalculateAttacksCount evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
            if (evt.Target.HPLeft <= 0)
                evt.Initiator.Resources.Restore(Swashbuckler.panache_resource, 1);
        }
    }
}
