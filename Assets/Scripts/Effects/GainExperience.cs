using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainExperience : CreatureEffect
{
    public int amount;

    public override void Run() {
        if (Creature != null) {
            Creature.Experience += amount;
        }
    }
}
