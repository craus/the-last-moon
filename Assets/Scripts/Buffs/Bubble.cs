using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Buff, IAttackModifier, IEndCombatModifier
{
    public int Priority => 70;

    public int thresholdDamage = 0;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner && attack.damage > thresholdDamage) {
            attack.damage = 0;
            Spend();
        }
    }

    public void OnCombatEnd() {
        Expire();
    }
}
