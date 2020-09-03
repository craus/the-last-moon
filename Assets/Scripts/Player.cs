using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Creature
{
    public static Player instance;

    public Transform abilitiesFolder;

    Action plannedAction;

    public void Awake() {
        instance = this;
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
        a.transform.SetParent(abilitiesFolder);
        a.transform.localScale = Vector3.one;
        a.gameObject.SetActive(true);
    }

    public void LoseAbility(Ability a) {
        Destroy(a.gameObject);
    }

    public void MakeMove(Ability a, Creature target) {
        plannedAction = () => UseAbility(a, target);
        MakeMove();
    }

    public override void AfterMove() {
        base.AfterMove();
        Battle.instance.NextRound();
        if (stunned < 0) {
            stunned++;
        } else {
            FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
        }
    }

    public override void TakeAction() {
        plannedAction();
    }

    protected override void OnBattleEnd(Battle b) {
        base.OnBattleEnd(b);
        if (Battle.instance.AllMonstersDead) {
            gold += 2;
        }
    }
}
