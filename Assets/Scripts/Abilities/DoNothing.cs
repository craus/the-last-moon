using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNothing : NonTargetEffect
{
    public override void Use(Creature user) {
    }

    public override string Text(Creature user) {
        return "-";
    }

    public override string Description(Creature user) {
        return $"Does nothing";
    }
}
