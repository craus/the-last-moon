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
        
    }

    public void GainAbility(Ability a) {
        a.transform.SetParent(abilitiesFolder);
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

    protected override void OnBattleEnd(Battle b) {
        base.OnBattleEnd(b);
        if (Battle.instance.AllMonstersDead) {
            gold += 1;
        }
    }
}
