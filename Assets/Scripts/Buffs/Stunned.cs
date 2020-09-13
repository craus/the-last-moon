using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : Buff
{
    public void ModifyMove() {
        var oldOwner = owner;
        Spend();
        GameLog.Message(power > 0 ? $"{oldOwner} loses 1 stun" : $"{oldOwner} is no longer stunned");
    }
}
