using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Creature
{
    private static Player _instance;

    public static Player instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

    public Transform abilitiesFolder;

    public void Awake() {
        _instance = this;
    }

    public override void Die(AbilityEffect source) {
        Game.instance.EndGame();
    }

    public void GainAbility(Ability a) {
        var spend = a.GetComponent<Spend>();
        if (spend != null) {
            var oldAbility = GetComponentsInChildren<Ability>().FirstOrDefault(a2 => a2.name == a.name);
            if (oldAbility != null) {
                oldAbility.GetComponent<Spend>().usages += spend.usages;
                Destroy(a.gameObject);
                return;
            }
        }
        var usagesPerBattle = a.GetComponent<UsagesPerBattle>();
        if (usagesPerBattle != null) {
            var oldAbility = GetComponentsInChildren<Ability>().FirstOrDefault(a2 => a2.name == a.name);
            if (oldAbility != null) {
                oldAbility.GetComponent<UsagesPerBattle>().usagesPerBattle += usagesPerBattle.usagesPerBattle;
                Destroy(a.gameObject);
                return;
            }
        }
        a.transform.SetParent(abilitiesFolder);
        a.transform.localScale = Vector3.one;
        a.gameObject.SetActive(true);
    }

    public void LoseAbility(Ability a) {
        Destroy(a.gameObject);
    }

    public void MakeMove(Ability a, Creature target) {
        GameManager.instance.PlanProcess(() => MakeMove(() => UseAbility(a, target)));
    }

    public override IPromise AfterMove() {
        return base.AfterMove().Then(() => {
            if (buffPower<Stunned>() < 0) {
                ApplyBuff<Stunned>(1);
                return Promise.Resolved();
            } else {
                var monsters = FindObjectsOfType<Monster>();
                var monstersMove = Promise.Sequence(monsters.Select<Monster, Func<IPromise>>(m => m.MakeMonsterMove));
                return monstersMove.Then(() => {
                    if (Game.instance.battle != null) {
                        Game.instance.battle.NextRound();
                    }
                });
            }
        });
    }

    protected override void OnBattleEnd(Battle b) {
        base.OnBattleEnd(b);
        if (Game.instance.battle.AllMonstersDead) {
            var reward = Game.instance.goldForBattleWin.rndRound();
            gold += reward;
            GameLog.Message($"Gained {reward} gold");
        }
    }
}
