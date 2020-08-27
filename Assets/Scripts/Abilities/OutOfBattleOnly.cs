using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBattleOnly : NonTargetEffect
{
    public override bool AllowUsage(Creature user) => !Battle.On;

    public override string Text(Creature user) {
        return "_";
    }
}
