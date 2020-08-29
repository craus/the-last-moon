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

    public override string Text(Creature user) {
        return $"h{(heal >= 100500 ? "∞" : heal.ToString())}";
    }

    public override string Description(Creature user) {
        if (heal >= 100500) {
            return $"Full heal";
        } else {
            return $"Heal yourself by {heal}";
        }
    }
}
