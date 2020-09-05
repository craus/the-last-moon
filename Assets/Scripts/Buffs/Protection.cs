using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : Buff, IAttackModifier
{
    public int Priority => 80;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            var protectedDamage = Mathf.Min(attack.damage, power);
            attack.damage -= protectedDamage;
            Spend(protectedDamage);
        }
    }
}
