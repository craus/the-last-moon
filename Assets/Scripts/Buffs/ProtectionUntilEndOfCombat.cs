using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionUntilEndOfCombat : Protection, IEndCombatModifier
{
    public override int Priority => 75;

    public void OnCombatEnd() {
        Expire();
    }
}
