using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BluntSaber : AbilityEffect
{
    public Counter counter;

    int Power(Creature user) {
        return counter.Value;
    }

    public override void Use(Creature user, Creature target) {
        int power = Power(user);
        counter.Decrement();
        target.Hit(user, power);
    }

    public override string Text(Creature user) {
        return "{0}--".i(Power(user));
    }

    public override string Description(Creature user) {
        return $"Deal {Power(user)} damage.\nLose 1 damage after each use.";
    }
}
