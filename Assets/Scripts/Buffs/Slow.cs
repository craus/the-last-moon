using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Buff, IAttackModifier
{
    public int Priority => 0;

    public void ModifyAttack(Attack attack) {
        if (attack.attacker == owner) {
            attack.Does(() => {
                owner.ApplyBuff<Stunned>(power);
            });
        }
    }
}
