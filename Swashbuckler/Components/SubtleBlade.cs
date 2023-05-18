using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class SubtleBlade : UnitFactComponentDelegate, ITargetRulebookHandler<RuleCombatManeuver>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCombatManeuver evt)
        {
            if ((evt.Type != CombatManeuver.Disarm && evt.Type != CombatManeuver.SunderArmor) || evt.Target.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;

            ItemEntityWeapon maybeWeapon = evt.Target.Body.PrimaryHand.MaybeWeapon;
            ItemEntityWeapon maybeWeapon2 = evt.Target.Body.SecondaryHand.MaybeWeapon;
            if ((maybeWeapon != null && !maybeWeapon.Blueprint.IsUnarmed && !maybeWeapon.Blueprint.IsNatural) && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(maybeWeapon.Blueprint, evt.Target.Descriptor))
            {
                evt.AutoFailure = true;
            }
            else if ((maybeWeapon2 != null && !maybeWeapon2.Blueprint.IsUnarmed && !maybeWeapon2.Blueprint.IsNatural) && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(maybeWeapon.Blueprint, evt.Target.Descriptor))
            {
                evt.AutoFailure = true;
            }
        }

        public void OnEventDidTrigger(RuleCombatManeuver evt)
        {
        }
    }
}
