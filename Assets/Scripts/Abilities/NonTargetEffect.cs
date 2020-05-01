using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonTargetEffect : AbilityEffect
{
    public override bool RequireTarget => false;

    public sealed override void Use(Creature user, Creature target) {
        Use(user);
    }

    public abstract void Use(Creature user);
}
