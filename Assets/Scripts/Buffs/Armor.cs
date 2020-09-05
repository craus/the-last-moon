using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Buff, IAttackModifier
{
    public int Priority => 50;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            attack.damage = Mathf.Clamp(attack.damage - power, 0, int.MaxValue);
        }
    }
}
