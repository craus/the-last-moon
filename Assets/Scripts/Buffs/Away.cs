using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Away : Buff, IAttackModifier
{
    public int Priority => 30;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            attack.interrupted = true;
        }
    }
}
