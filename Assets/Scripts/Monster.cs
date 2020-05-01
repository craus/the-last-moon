using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    public override void TakeAction() {
        Attack(Battle.instance.player);
    }
}
