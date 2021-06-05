using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public event Action onWin = () => { };

    public int moveNumber;
    public bool on = true;

    public GameObject startBattleImage;

    public void PlanStartBattle() {
        GameManager.instance.PlanProcess(StartBattle);
    }

    private IPromise StartBattle() {
        startBattleImage.SetActive(true);
        return TimeManager.Wait(0.1f).Then(() => {
            startBattleImage.SetActive(false);
            GlobalEvents.instance.onBattleStart.Invoke(this);
            GameLog.LogBattleRound();
        });
    }

    public void Win() {
        Finish();
        onWin();
    }

    public void Finish() {
        GlobalEvents.instance.onBattleEnd(this);
        Destroy(gameObject);
        on = false;
        //AbilitiesController.instance.currentAbility = null;
        Game.instance.battle = null;
    }

    public bool AllMonstersDead => FindObjectsOfType<Monster>().All(m => m.Dead);

    public void NextRound() {
        if (!on) {
            return;
        }
        moveNumber++;
        GameLog.LogBattleRound();
    }
}
