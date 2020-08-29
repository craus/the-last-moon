using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDamage : NonTargetEffect
{
    public int damage;

    public override void Use(Creature user) {
        user.LoseHp(damage);
    }

    public override string Text(Creature user) {
        return "X" + damage;
    }

    public override string Description(Creature user) {
        return $"Damage yourself by {damage}.";
    }
}
