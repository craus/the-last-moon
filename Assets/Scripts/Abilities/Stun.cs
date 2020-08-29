using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : AbilityEffect
{
    public int stun;

    public override void Use(Creature user, Creature target) {
        target.Stun(stun);
    }

    public override string Text(Creature user) {
        return CreatureText.StunString(stun);
    }

    public override string Description(Creature user) {
        return $"Stun for {stun} turns";
    }
}
