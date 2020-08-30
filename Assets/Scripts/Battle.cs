using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public static bool On => instance != null && instance.on;

    public int moveNumber;
    public bool on = true;

    public void Finish() {
        GlobalEvents.instance.onBattleEnd(this);
        Destroy(gameObject);
        on = false;
        AbilitiesController.instance.currentAbility = null;
    }

    public bool AllMonstersDead => FindObjectsOfType<Monster>().All(m => m.Dead);

    public void Start() {
        GlobalEvents.instance.onBattleStart(this);
        GameLog.Message($"Battle started - Day {Game.instance.day}");
    }

    public void NextRound() {
        if (!on) {
            return;
        }
        moveNumber++;
        GameLog.LogBattleRound();
    }
}
