using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSplashDamage : AbilityEffect
{
    public int damage;

    public override void Use(Creature user, Creature target) {
        FindObjectsOfType<Monster>().ForEach(m => m.Hit(damage));
    }
}
