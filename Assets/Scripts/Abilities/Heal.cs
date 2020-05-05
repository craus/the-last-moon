using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : AbilityEffect
{
    public int heal;
    public override bool BattleOnly => false;

    public override void Use(Creature user, Creature target) {
        target.Heal(heal);
    }

    public override string Text(Creature user) {
        return "H" + heal.ToString();
    }
}
