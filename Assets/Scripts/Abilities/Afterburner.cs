﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : NonTargetEffect
{
    public int power;

    public override void Use(Creature user) {
        user.Afterburner(power);
    }

    public override string Text() {
        return CreatureText.StunString(-power);
    }
}