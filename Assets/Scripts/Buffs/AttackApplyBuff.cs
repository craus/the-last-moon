using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackApplyBuff : Buff, IAttackModifier
{
    public Buff buff;

    public int Priority => 120;

    public void ModifyAttack(Attack attack) {
        if (attack.attacker == owner) {
            buff.Apply(attack.victim);
        }
    }
}
