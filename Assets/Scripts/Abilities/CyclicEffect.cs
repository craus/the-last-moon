using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclicEffect : AbilityEffect
{
    public List<AbilityEffect> effects;
    public AbilityEffect current;

    public override void Use(Creature user, Creature target) {
        current.Use(user, target);
        current = effects.CyclicNext(current);
    }
}
