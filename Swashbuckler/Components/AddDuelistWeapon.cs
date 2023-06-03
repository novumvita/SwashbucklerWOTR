using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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