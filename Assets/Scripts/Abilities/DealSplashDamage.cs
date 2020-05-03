using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSplashDamage : NonTargetEffect
{
    public int damage;

    public override void Use(Creature user) {
        FindObjectsOfType<Monster>().ForEach(m => m.Hit(damage));
    }

    public override string Text() {
        return "S" + damage.ToString();
    }
}
