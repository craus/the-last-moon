using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendGold : NonTargetEffect
{
    public override bool BattleOnly => false;

    public override bool AllowUsage(Creature user) => user.gold >= amount;

    public int amount;

    public override void Use(Creature user) {
        user.gold -= amount;
        user.gold = Mathf.Clamp(user.gold, 0, int.MaxValue);
    }

    public override string Text(Creature user) {
        return " <color=#ffa000ff>(${0})</color>".i(amount);
    }
}
