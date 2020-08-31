using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendSkillPoint : NonTargetEffect
{
    public override bool BattleOnly => false;

    public override bool AllowUsage(Creature user) => user.skillPoints >= amount;

    public int amount;

    public override void Use(Creature user) {
        user.skillPoints -= amount;
    }

    public override string Text(Creature user) {
        return $"<color=#328B28ff>({Texts.sp(amount)})</color>";
    }

    public override string Description(Creature user) {
        return $"<color=#328B28ff>({Texts.skillPoints(amount)})</color>";
    }
}
