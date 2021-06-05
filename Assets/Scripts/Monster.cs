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
        return name;
        //return CreatureText.FormatCreature(FORMAT, this);
    }

    private string Var(string name, int value) => value == 0 ? "" : $"{name}: {value}\n";

    public string Description() {
        return 
            $"attack damage: {damage}\n" +
            $"health: {hp}/{maxHp}\n" +
            $"{Var("away from battle", buffPower<Away>())}" +
            $"{Var("regeneration", buffPower<Regeneration>())}" +
            $"{Var("protection", buffPower<Protection>())}" +
            $"{Var("stunned", buffPower<Stunned>())}" +
            $"{Var("slow", slow)}" +
            $"{Var("armor", buffPower<Armor>())}" +
            $"{Var("counterattack", buffPower<CounterAttack>())}" +
            $"{Var("bubbles", buffPower<Bubble>())}" +
            $"{Var("regeneration", buffPower<Regeneration>())}" +
            $"";
    }
}
