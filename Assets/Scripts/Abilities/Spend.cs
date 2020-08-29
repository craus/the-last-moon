using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spend : NonTargetEffect
{
    public override bool AllowUsage(Creature user) => usages >= 1;

    public int usages;

    public override void Use(Creature user) {
        usages -= 1;
    }

    public override string Text(Creature user) {
        return " ({0})".i((int)usages);
    }

    public override string Description(Creature user) {
        return $"{usages} usages";
    }
}
