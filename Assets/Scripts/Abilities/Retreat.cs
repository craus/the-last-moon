using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : AbilityEffect
{
    public override bool RequireTarget => false;

    public override void Use(Creature user, Creature target) {
        Game.instance.battle.Finish();
    }

    public override string Text(Creature user) {
        return "<—";
    }

    public override string Description(Creature user) {
        return "Retreat from combat (no rewards)";
    }
}
