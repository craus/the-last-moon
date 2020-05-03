using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHeal : NonTargetEffect
{
    public int heal;
    public override bool BattleOnly => false;

    public override void Use(Creature user) {
        user.Heal(heal);
    }

    public override string Text() {
        return "h" + heal.ToString();
    }
}
