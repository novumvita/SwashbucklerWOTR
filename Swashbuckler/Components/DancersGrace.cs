using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.Items.Slots;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using UnityEngine;

namespace Swashbuckler.Components
{
    internal class DancersGrace : UnitFactComponentDelegate, IUnitActiveEquipmentSetHandler, IGlobalSubscriber, ISubscriber, IUnitEquipmentHandler
    {
        public override void OnTurnOn()
        {
            base.OnTurnOn();
            CheckConditions();
        }

        public override void OnTurnOff()
        {
            base.OnTurnOff();
            DeactivateModifier();
        }

        public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
        {
            CheckConditions();
        }

        public void CheckConditions()
        {
            if (CheckArmor())
            {
                ActivateModifier();
            }
            else
            {
                DeactivateModifier();
            }
        }

        public bool CheckArmor()
        {
            return !Owner.Body.Armor.HasArmor && !Owner.Body.SecondaryHand.HasShield;
        }

        public void ActivateModifier()
        {
            int value = Math.Min(base.Owner.Stats.Charisma.Bonus, base.Owner.Progression.GetClassLevel(Swashbuckler.swash_class));
            base.Owner.Stats.AC.AddModifierUnique(value, base.Runtime, ModifierDescriptor.Dodge);
        }

        public void DeactivateModifier()
        {
            base.Owner.Stats.AC.RemoveModifiersFrom(base.Runtime);
        }

        public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
        {
            if (!(slot.Owner != base.Owner))
            {
                CheckConditions();
            }
        }
    }
}
