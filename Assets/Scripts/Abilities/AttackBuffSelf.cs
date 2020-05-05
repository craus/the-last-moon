using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffSelf : NonTargetEffect
{
    public int attack;

    public override void Use(Creature user) {
        user.attack += attack;
    }

    public override string Text(Creature user) {
        return CreatureText.AttackString(attack);
    }
}
