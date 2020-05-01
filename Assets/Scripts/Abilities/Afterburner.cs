using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : AbilityEffect
{
    public int power;

    public override void Use(Creature user, Creature target) {
        target.Afterburner(power);
    }
}
