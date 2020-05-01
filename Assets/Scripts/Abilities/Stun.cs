using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : AbilityEffect
{
    public int stun;

    public override void Use(Creature user, Creature target) {
        target.Stun(stun);
    }
}
