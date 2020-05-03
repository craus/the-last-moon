using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventAbilityEffect : AbilityEffect
{
    public UnityEvent use;

    public override void Use(Creature user, Creature target) {
        use.Invoke();
    }
}
