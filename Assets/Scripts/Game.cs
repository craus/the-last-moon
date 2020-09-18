using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singletone<Game>
{
    public int day;

    public Battle battleSample;
    public Store storeSample;
    public Transform storeSlot;
    public Store store;
    public Player player;
    public Battle battle;
    public bool battleOn => battle != null && battle.on;
    public bool storeOn => store != null;

    public void DestroyBattle() {
        if (battle != null) {
            Destroy(battle.gameObject);
        }
    }

    public void NextDay() {
        day++;
        Statistics.RegisterCurrentDay(day);
    }

    public void NewBattle() {
        DestroyStore();
        NextDay();
        battle = Instantiate(battleSample, transform);
        var spawner = battle.GetComponentInChildren<MonsterSpawner>();
        spawner.mana += Game.instance.day * spawner.manaPerGameDay;
    }

    public void NewStore() {
        DestroyStore();
        store = Instantiate(storeSample, storeSlot);
    }

    public void DestroyStore() {
        if (store != null) {
            Destroy(store.gameObject);
            store = null;
        }
    }

    public void RestartBattle() {
        if (battle != null) {
            battle.Finish();
        }
        NewBattle();
    }

    public void Click(Creature creature) {
        if (AbilitiesController.instance.currentAbility != null) {
            GameManager.instance.PlanInstantProcess(() => {
                PlayerUseAbility(AbilitiesController.instance.currentAbility, creature);
            });
        }
    }

    public void ClickAbility(Ability ability) {
        if (Game.instance.battle == null && ability.BattleOnly) {
            return;
        }
        if (!ability.RequireTarget) {
            GameManager.instance.PlanInstantProcess(() => {
                PlayerUseAbility(ability);
            });
        } else {
            AbilitiesController.instance.currentAbility = ability;
        }
    }

    public void PlayerUseAbility(Ability a, Creature t = null) {
        if (Game.instance.battle != null) {
            player.MakeMove(a, t);
        } else {
            player.UseAbility(a, t);
        }
        if (!a.Available(player) && AbilitiesController.instance.currentAbility == a) {
            AbilitiesController.instance.currentAbility = null;
        }
    }

    public void EndGame() {
        DestroyStore();
        Statistics.RegisterDeath();
    }

    public void Start() {
        Statistics.RegisterNewRun();
        GlobalEvents.instance.onGameStart.Invoke(this);
        NewStore();
        GameLog.Message("Game started");

        GlobalEvents.instance.onBattleEnd += OnBattleEnd;
    }

    int daysWithNoStores = 0;
    public void OnBattleEnd(Battle battle) {
        if (!Player.instance.Alive) {
            return;
        }
        if (Rand.rndEvent(0.16f * daysWithNoStores)) {
            NewStore();
            daysWithNoStores = 0;
        } else {
            daysWithNoStores++;
        }
    }
}
