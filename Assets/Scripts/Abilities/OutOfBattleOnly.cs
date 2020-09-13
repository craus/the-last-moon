using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBattleOnly : NonTargetEffect
{
    public override bool AllowUsage(Creature user) => !Game.instance.battleOn;

    public override string Text(Creature user) {
        return "_";
    }

    public override string Description(Creature user) {
        return "Out of battle only.";
    }
}
