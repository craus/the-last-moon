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
        return $"Next damage dealt this battle is increased by {power}";
    }

    public override string Text() {
        return CreatureText.AttackString("na", power);
    }
}
