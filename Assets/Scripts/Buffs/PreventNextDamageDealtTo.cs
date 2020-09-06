using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Prevents next damage buffed creature would deal to target creature
/// </summary>
public class PreventNextDamageDealtTo : Buff, IAttackModifier
{
    public CreatureProvider target;

    public int Priority => 60;

    public void ModifyAttack(Attack attack) {
        DebugManager.LogFormat("PreventNextDamageDealtTo.ModifyAttack");
        if (attack.attacker == owner && attack.victim == target.Value) {
            DebugManager.LogFormat("attack.attacker == owner && attack.victim == target.Value");
            attack.damage = 0;
            Spend();
            GameLog.Message($"Prevented attack damage from {attack.attacker.Text()} to {attack.victim.Text()}");
        }
    }

    public override string Text() {
        return "!";
    }
}
