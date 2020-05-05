using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spend : NonTargetEffect
{
    public override bool AllowUsage => usages >= 1;

    public int usages;
    public Ability ability;

    public override void Use(Creature user) {
        usages -= 1;
    }

    public override string Text(Creature user) {
        return " ({0})".i((int)usages);
    }
}
