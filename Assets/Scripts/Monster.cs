using RSG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : Creature
{
    public const string FORMAT = "{away}{damage}/{hp}{status}";

    public override void Die(AbilityEffect source) {
        base.Die(source);
        if (Game.instance.battle.AllMonstersDead) {
            GameManager.instance.PlanAction(Game.instance.battle.Win);
        }
        Player.instance.Experience += 1;
    }

    public IPromise MakeMonsterMove() {
        return MakeMove(TakeAction);
    }

    public virtual void TakeAction() {
        Attack(Game.instance.player);
    }

    public override string Text() {
        return CreatureText.FormatCreature(FORMAT, this);
    }
}
