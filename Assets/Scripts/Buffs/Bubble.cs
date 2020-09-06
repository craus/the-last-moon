﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : Buff, IAttackModifier
{
    public int Priority => 70;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            attack.damage = 0;
            GameLog.Message($"{owner.Text()} loses 1 bubble");
            Spend();
        }
    }
}
