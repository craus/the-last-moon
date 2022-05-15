using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Buff, IEndTurnModifier, IEndCombatModifier
{
    public int Priority => 70;

    public void OnTurnEnd() {
        owner.Heal(Power);
    }

    public virtual void OnCombatEnd() {
        owner.Heal(9999999);
    }
}
