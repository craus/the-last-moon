using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainBubble : Common.Effect
{
    public Creature creature;

    public override void Run() {
        if (creature != null) {
            creature.bubbles++;
        }
    }
}
