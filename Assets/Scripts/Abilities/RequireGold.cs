using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireGold : NonTargetEffect
{
    public override bool AllowUsage(Creature user) => user.gold >= threshold;

    public int threshold;

    public override string Text(Creature user) {
        return " <color=#ffa000ff>(${0})</color>".i(threshold);
    }
}
