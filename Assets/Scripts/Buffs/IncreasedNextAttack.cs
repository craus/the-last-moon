using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedNextAttack : IncreasedAttack
{
    protected override void IncreaseAttack(Attack attack) {
        base.IncreaseAttack(attack);
        attack.Does(Expire);
    }

    public override string Description() {
        return $"Increase next damage dealt this battle by {power}";
    }

    public override string Text() {
        return CreatureText.AttackString("na", power);
    }
}
