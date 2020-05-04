using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningRing : AbilityEffect
{
    public override bool Available => base.Available && counter.Value > 0;

    public Counter counter;

    public override void Use(Creature user, Creature target) {
        if (!Available) {
            return;
        }
        int power = counter.Value;
        counter.Reset();
        target.Hit(power);
    }

    public override string Text() {
        return "<color=#ff8000ff>{0}</color>".i(counter.Value);
    }
}
