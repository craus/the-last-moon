using RSG;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public static bool On => instance != null && instance.on;

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
            GlobalEvents.instance.onBattleStart.Invoke(Battle.instance);
            GameLog.LogBattleRound();
        });
    }

    public void Finish() {
        GlobalEvents.instance.onBattleEnd(this);
        Destroy(gameObject);
        on = false;
        AbilitiesController.instance.currentAbility = null;
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
