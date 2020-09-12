using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : NonTargetEffect
{
    public int power;

    public override void Use(Creature user) {
        user.ApplyBuff<Stunned>(-power);
    }

    public override string Text(Creature user) {
        return CreatureText.StunString(-power);
    }

    public override string Description(Creature user) {
        return $"Gain {power} extra turns";
    }
}
