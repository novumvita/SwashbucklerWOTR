using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace Swashbuckler.Components
{
    internal class SwashbucklerPreciseStrike : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if (Owner.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1 || !SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator) || (Owner.Body.SecondaryHand.HasWeapon && base.Owner.Body.SecondaryHand.MaybeWeapon != base.Owner.Body.EmptyHandWeapon) || (Owner.Body.SecondaryHand.HasShield && Owner.Body.SecondaryHand.MaybeShield.ArmorComponent.Blueprint.ProficiencyGroup != Kingmaker.Blueprints.Items.Armors.ArmorProficiencyGroup.Buckler))
                return;

            int swash_level = Owner.Progression.GetClassLevel(Swashbuckler.swash_class);
            evt.PreciseStrike += Owner.Descriptor.HasFact(Swashbuckler.precise_strike_buff) ? 2 * swash_level : swash_level;
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
        }
    }
}
