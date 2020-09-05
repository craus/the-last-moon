using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBuffSelf : NonTargetEffect
{
    public int attack;

    public override void Use(Creature user) {
        user.ApplyBuff<IncreasedAttack>(attack);
    }

    public override string Text(Creature user) {
        return CreatureText.AttackString(attack);
    }

    public override string Description(Creature user) {
        return $"Increase all damage dealt by {attack} until end of battle";
    }
}
