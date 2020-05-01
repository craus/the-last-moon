using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHeal : NonTargetEffect
{
    public int heal;

    public override void Use(Creature user) {
        user.Heal(heal);
    }
}
