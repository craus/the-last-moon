using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Buff, IAttackModifier
{
    public int Priority => 100;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            if (attack.damageType != DamageType.Thorns) {
                attack.attacker.Hit(owner, power, damageType: DamageType.Thorns);
            }
        }
    }
}
