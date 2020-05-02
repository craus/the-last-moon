using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    Action plannedAction;

    public override void Die() {
        
    }

    public void MakeMove(Ability a, Creature target) {
        plannedAction = () => UseAbility(a, target);
        MakeMove();
    }

    public override void AfterMove() {
        base.AfterMove();
        Battle.instance.moveNumber++;
        if (stunned < 0) {
            stunned++;
        } else {
            FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
        }
    }

    public override void TakeAction() {
        plannedAction();
    }
}
