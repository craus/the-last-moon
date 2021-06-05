using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoseHpFromAttack : Buff, IAttackModifier
{
    public int Priority => 90;

    public UnityEvent onLoseHp;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            if (attack.damage > 0) {
                GlobalEvents.instance.onHit(attack.victim, attack.damage, attack.source);
                onLoseHp.Invoke();
            }
            owner.LoseHp(attack.damage, attack.source, attack.attacker);
        }
    }

    public override string Description() {
        return "Mortal";
    }
}
