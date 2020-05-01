using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDamage : AbilityEffect
{
    public int damage;

    public override void Use(Creature user, Creature target) {
        user.LoseHp(damage);
    }
}
