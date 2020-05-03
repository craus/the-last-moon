using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireRing : AbilityEffect
{
    public override bool Available => base.Available && counter.Value > 0;

    public Counter counter;

    public override void Use(Creature user, Creature target) {
        int power = 1;
        counter.Decrement();
        target.Hit(power);
        user.Afterburner(1);
    }

    public override string Text() {
        return "1f (<color=#ff0000ff>{0}</color>)".i(counter.Value);
    }
}
