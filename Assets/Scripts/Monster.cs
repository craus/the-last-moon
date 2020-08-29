using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Creature
{
    public override void TakeAction() {
        Attack(Game.instance.player);
    }

    public override void Die(AbilityEffect source) {
        base.Die(source);
        if (Battle.instance.AllMonstersDead) {
            Battle.instance.Finish();
        }
    }
}
