using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spend : AbilityEffect
{
    public int usages;
    public Ability ability;

    public override void Use(Creature user, Creature target) {
        usages--;
        if (usages == 0) {
            Destroy(ability.gameObject);
        }
    }
}
