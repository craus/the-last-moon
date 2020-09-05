using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationUntilEndOfCombat : Regeneration
{
    public override void OnCombatEnd() {
        base.OnCombatEnd();
        Expire();
    }
}
