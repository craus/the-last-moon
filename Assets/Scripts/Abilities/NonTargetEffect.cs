using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonTargetEffect : AbilityEffect
{
    public override bool RequireTarget => false;

    public sealed override void Use(Creature user, Creature target) {
        base.Use(user, target);
        Use(user);
    }

    public virtual void Use(Creature user) {
    }
}
