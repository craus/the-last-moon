﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSplashDamage : NonTargetEffect
{
    public int damage;

    public override void Use(Creature user) {
        FindObjectsOfType<Monster>().ForEach(m => m.Hit(user, damage));
    }

    public override string Text(Creature user) {
        return "S" + (damage).ToString();
    }
}
