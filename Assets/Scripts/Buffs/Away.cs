using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Away : Buff, IAttackModifier, IEndCombatModifier
{
    public int Priority => 30;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            attack.interrupted = true;
        }
    }

    public void ModifyMove() {
        Spend();
    }

    public override void LogSpend(int delta, int oldPower) {
        GameLog.Message(power > 0 ? $"{owner} is getting closer to battle ({oldPower} -> {power})" : $"{owner} enters the battle");
    }

    public void OnCombatEnd() {
        Expire();
    }
}
