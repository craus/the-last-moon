using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spend : NonTargetEffect
{
    public override bool AllowUsage(Creature user) => usages >= 1;

    public int usages;

    public bool destroyOnUsagesEnd = false;

    public override void Use(Creature user) {
        usages -= 1;
        if (usages == 0 && destroyOnUsagesEnd) {
            Destroy(gameObject);
        }
    }

    public override string Text(Creature user) {
        return " ({0})".i((int)usages);
    }

    public override string Description(Creature user) {
        return $"{usages} usages";
    }
}
