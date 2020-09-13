using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedAttack : Buff, IAttackModifier, IEndCombatModifier
{
    public int Priority => 40;

    public void ModifyAttack(Attack attack) {
        if (attack.attacker == owner) {
            if (attack.damageType != DamageType.Thorns) {
                IncreaseAttack(attack);
            }
        }
    }

    protected virtual void IncreaseAttack(Attack attack) {
        var old = attack.damage;
        attack.damage = Mathf.Clamp(attack.damage + power, 0, int.MaxValue);
        var delta = attack.damage - old;

        GameLog.Message($"Attack damage modified by {delta}");
    }

    public void OnCombatEnd() {
        Expire();
    }
}
