using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Buff, IAttackModifier
{
    public int Priority => 50;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            var old = attack.damage;
            attack.damage = Mathf.Clamp(attack.damage - power, 0, int.MaxValue);
            var delta = attack.damage - old;
            if (delta < 0) {
                GameLog.Message($"Armor reduces damage by {-delta} ({old} -> {attack.damage})");
            } else {
                GameLog.Message($"Vulnerability increases damage by {delta} ({old} -> {attack.damage})");
            }
        }
    }
}
