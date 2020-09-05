﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSplashDamageEffect : CreatureEffect
{
    public int damage;

    public override void Run(Creature creature) {
        base.Run(creature);

        FindObjectsOfType<Monster>().ForEach(m => m.Hit(creature, damage));
    }

    public override void Run() {
        DebugManager.LogFormat("DealSplashDamageEffect.Run");
        DebugManager.LogFormat("Monsters: {0}", FindObjectsOfType<Monster>());
        FindObjectsOfType<Monster>().ForEach(m => m.Hit(Creature, damage));
    }
}
