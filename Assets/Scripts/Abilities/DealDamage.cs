using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : AbilityEffect
{
    public int damage;

    public override void Use(Creature user, Creature target) {
        target.Hit(damage);
    }
}
