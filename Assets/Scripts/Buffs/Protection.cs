using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : Buff, IAttackModifier
{
    public virtual int Priority => 80;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            var protectedDamage = Mathf.Min(attack.damage, power);
            ModifyAttackDamage(attack, -protectedDamage);
            Spend(protectedDamage);
        }
    }
}
