using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventAbilityEffect : NonTargetEffect
{
    public UnityEvent use;

    public override void Use(Creature user) {
        use.Invoke();
    }
}
