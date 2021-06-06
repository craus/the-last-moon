using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedNextAttackFrom : IncreasedNextAttack
{
    public AbilityEffect source;

    protected override void IncreaseAttack(Attack attack) {
        if (attack.source == source) {
            base.IncreaseAttack(attack);
        }
    }

    public override string Description() {
        return $"Next damage dealt by {source} this battle is increased by {power}";
    }

    public override string Text() {
        return CreatureText.AttackString($"{source.Text(owner)}na", power);
    }
}
