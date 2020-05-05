using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfProtection : NonTargetEffect
{
    public int protection;

    public override void Use(Creature user) {
        user.protectionUntilEndOfCombat += protection;
    }

    public override string Text(Creature user) {
        return CreatureText.ProtectionString(protection);
    }
}
