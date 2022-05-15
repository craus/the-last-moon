using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : Buff
{
    public void ModifyMove() {
        Spend();
    }

    public override void LogSpend(int delta, int oldPower) {
        //GameLog.Message(Power > 0 ? $"{owner} loses 1 stun" : $"{owner} is no longer stunned");
    }
}
