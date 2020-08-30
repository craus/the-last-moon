using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : CreatureEffect
{
    public int amount;

    public override void Run() {
        RunByAmount(amount);
    }

    public void RunByAmount(int amount) {
        if (Creature != null) {
            Creature.Heal(amount);
        }
    }
}
