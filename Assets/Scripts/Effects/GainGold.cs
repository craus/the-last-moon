﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainGold : Common.Effect
{
    public Creature creature;
    public int amount;

    public override void Run() {
        if (creature != null) {
            creature.gold += amount;
        }
    }
}
