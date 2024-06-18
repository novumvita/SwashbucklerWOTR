using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;

namespace Swashbuckler.Components
{
    public class AddDuelistWeapon : UnitFactComponentDelegate
    {
        public WeaponCategory WeaponCategory;

        public override void OnTurnOn()
        {
            this.Owner.Ensure<UnitPartDamageGrace>().AddEntry(this.WeaponCategory, this.Fact);
        }

        public override void OnTurnOff()
        {
            this.Owner.Ensure<UnitPartDamageGrace>().RemoveEntry(this.Fact);
        }
    }
}