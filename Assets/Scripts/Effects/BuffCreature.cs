using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCreature : CreatureEffect
{
    public Buff buff;

    public override void Run() {
        if (Creature != null) {
            buff.Apply(Creature);
        }
    }

    public override void Run(Creature creature) {
        base.Run(creature);
        buff.Apply(creature);
    }
}
