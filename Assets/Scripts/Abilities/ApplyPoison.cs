using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPoison : AbilityEffect
{
    public int poison;

    public override void Use(Creature user, Creature target) {
        target.Poison(poison);
    }

    public override string Text(Creature user) {
        return CreatureText.RegenerationString(-poison);
    }
}
