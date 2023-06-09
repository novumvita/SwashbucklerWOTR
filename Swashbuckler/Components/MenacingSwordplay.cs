﻿using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace Swashbuckler.Components
{
    internal class MenacingSwordplay : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (evt.Initiator.CombatState.Cooldown.SwiftAction != 0.0f || !SwashbucklerWeaponCalculations.IsSwashbucklerWeapon(evt.Weapon.Blueprint, evt.Initiator) || !evt.IsHit || evt.Initiator.Descriptor.Resources.GetResourceAmount(Swashbuckler.panache_resource) < 1)
                return;

            evt.Initiator.SpendAction(UnitCommand.CommandType.Swift, false, 0);
            base.Fact.RunActionInContext(ActionsBuilder.New().Add<Demoralize>(c => { c.m_Buff = BuffRefs.Shaken.Reference.Get().ToReference<BlueprintBuffReference>(); c.m_GreaterBuff = BuffRefs.Frightened.Reference.Get().ToReference<BlueprintBuffReference>(); }).Build(), evt.Target);
        }
    }
}
