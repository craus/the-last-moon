using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfProtection : NonTargetEffect
{
    public int protection;

    public override void Use(Creature user) {
        user.ApplyBuff<ProtectionUntilEndOfCombat>(protection);
    }

    public override string Text(Creature user) {
        return CreatureText.ProtectionString(protection);
    }

    public override string Description(Creature user) {
        return $"Prevents next {protection} damage taken this battle.";
    }
}
