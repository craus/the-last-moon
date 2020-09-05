using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainBubble : CreatureEffect
{
    public override void Run() {
        if (Creature != null) {
            Creature.ApplyBuff<Bubble>();
        }
    }
}
