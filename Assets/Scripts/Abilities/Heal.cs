using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : AbilityEffect
{
    public int heal;

    public override void Use(Creature user, Creature target) {
        target.Heal(heal);
    }
}
