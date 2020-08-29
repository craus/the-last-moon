using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : AbilityEffect
{
    public override bool RequireTarget => false;

    public override void Use(Creature user, Creature target) {
        Battle.instance.Finish();
    }

    public override string Text(Creature user) {
        return "<—";
    }
}
