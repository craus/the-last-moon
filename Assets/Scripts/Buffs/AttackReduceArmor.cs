using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReduceArmor : Buff, IAttackModifier
{
    public int Priority => 110;

    public void ModifyAttack(Attack attack) {
        if (attack.attacker == owner) {
            attack.Does(() => {
                attack.victim.ApplyBuff<Armor>(-Power);
            });
        }
    }
}
