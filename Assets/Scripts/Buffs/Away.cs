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

    public void ModifyMove() {
        var oldOwner = owner;
        Spend();
        GameLog.Message(power > 0 ? $"{oldOwner} is getting closer to battle" : $"{oldOwner} enters the battle");
    }
}
