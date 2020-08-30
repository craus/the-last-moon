using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Common.Effect
{
    public Creature creature;
    public CreatureProvider creatureProvider;

    public Creature Creature => creatureProvider != null ? creatureProvider.Value : creature;

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
