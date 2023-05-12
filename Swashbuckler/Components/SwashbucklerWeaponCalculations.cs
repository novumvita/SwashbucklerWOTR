using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckler.Components
{
    public class SwashbucklerWeaponCalculations
    {
        public static bool IsSwashbucklerWeapon(BlueprintItemWeapon weapon, UnitDescriptor wielder)
        {
            // Identical check for Duelist weapons
            if (weapon.IsMelee && (weapon.Category.HasSubCategory(WeaponSubCategory.Light) || weapon.Category.HasSubCategory(WeaponSubCategory.OneHandedPiercing) || (wielder.State.Features.DuelingMastery && weapon.Category == WeaponCategory.DuelingSword) || wielder.Ensure<UnitPartDamageGrace>().HasEntry(weapon.Category)))
            {
                return true;
            }
            return false;
        }
    }
    public class AttackStatReplacementForSwashbucklerWeapon : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, IInitiatorRulebookSubscriber
    {
        private StatType ReplacementStat = StatType.Dexterity;

        public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
            ModifiableValueAttributeStat stat1 = this.Owner.Stats.GetStat(evt.AttackBonusStat) as ModifiableValueAttributeStat;
            ModifiableValueAttributeStat stat2 = this.Owner.Stats.GetStat(this.ReplacementStat) as ModifiableValueAttributeStat;
            bool flag = stat2 != null && stat1 != null && stat2.Bonus >= stat1.Bonus;

            if (SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator.Descriptor) && flag)
            {
                evt.AttackBonusStat = this.ReplacementStat;
            }
        }

        public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt)
        {
        }
    }

    public class ImprovedCriticalOnWieldingSwashbucklerWeapon : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            if (evt.Weapon != null && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator.Descriptor))
            {
                evt.DoubleCriticalEdge = true;
            }
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }

    public class CritAutoconfirmWithSwashbucklerWeapons : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            if (SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator.Descriptor))
            {
                evt.AutoCriticalConfirmation = true;
            }
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
        }
    }

    public class IncreasedCriticalMultiplierWithSwashbucklerWeapon : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            if (evt.Weapon != null && SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator.Descriptor))
            {
                evt.AdditionalCriticalMultiplier.Add(new Modifier(1, base.Fact, ModifierDescriptor.UntypedStackable));
            }
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}