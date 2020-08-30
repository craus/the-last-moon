using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainBubble : Common.Effect
{
    public Creature creature;
    public CreatureProvider creatureProvider;

    public Creature Creature => creatureProvider != null ? creatureProvider.Value : creature;

    public override void Run() {
        if (Creature != null) {
            Creature.bubbles++;
        }
    }
}
