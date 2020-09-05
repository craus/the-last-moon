using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Prevents next damage buffed creature would deal to target creature
/// </summary>
public class PreventNextDamageDealtTo : Buff, IAttackModifier
{
    public Creature target;

    public int Priority => 60;

    public void ModifyAttack(Attack attack) {
        if (attack.attacker == owner && attack.victim == target) {
            attack.damage = 0;
            Spend();
        }
    }
}
