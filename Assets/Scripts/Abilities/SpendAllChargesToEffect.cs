using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpendAllChargesToEffect : AbilityEffect
{
    public override bool Available => base.Available && counter.Value > 0;

    public Counter counter;

    public CreatureIntCreatureEvent effect;

    int Power(Creature user) {
        return counter.Value;
    }

    public override void Use(Creature user, Creature target) {
        if (!Available) {
            return;
        }
        int power = Power(user);
        counter.Reset();
        effect.Invoke(target, power, user);
    }

    public override string Text(Creature user) {
        return "<color=#ff8000ff>{0}</color>".i(Power(user));
    }

    public override string Description(Creature user) {
        return $"Deal 1 damage for each charge. Spend all charges.\n{counter.Value} charges.\nGain 1 charge on enemy death.";
    }
}
