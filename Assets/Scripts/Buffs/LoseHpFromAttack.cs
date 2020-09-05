using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseHpFromAttack : Buff, IAttackModifier
{
    public int Priority => 90;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            owner.LoseHp(attack.damage, attack.source, attack.attacker);
        }
    }
}
