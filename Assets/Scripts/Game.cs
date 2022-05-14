using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float spawnerBaseMana = 1;
    public float spawnerManaPerDay = 0;//0.45f;
    public float spawnerManaMultiplierPerDay = 1.12f;
    public float goldMultiplierPerDay = 1.12f;
    public float spawnerBaseManaPerTurn = 2.15f;
    public float spawnerManaPerTurn2 = 0.0015f;
    public float baseGoldForBattleWin = 2f;

    public float goldForBattleWin => baseGoldForBattleWin * Mathf.Pow(goldMultiplierPerDay, day);

    public void DestroyBattle() {
        if (battle != null) {
            Destroy(battle.gameObject);
        }
    }

    public void NextDay() {
        day++;
        Statistics.UpdateCurrentRun(r => r.savedGame = Save());
    }

    public void NewBattle() {
        DestroyStore();
        NextDay();
        battle = Instantiate(battleSample, transform);
        var spawner = battle.GetComponentInChildren<MonsterSpawner>();
        spawner.mana = spawnerBaseMana;
        spawner.manaPerTurn = spawnerBaseManaPerTurn;
        spawner.mana += day * spawnerManaPerDay;
        spawner.mana *= Mathf.Pow(spawnerManaMultiplierPerDay, day);
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
        Debug.Log($"Check {a}");
        if (!a.Available(player) && AbilitiesController.instance.currentAbility == a) {
            AbilitiesController.instance.currentAbility = null;
        }
    }

    public void EndGame() {
        DestroyStore();
        Statistics.RegisterDeath();
    }

    public void Start() {
        Statistics.RegisterNewRun(Save());
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
        if (Rand.rndEvent(0.2f * daysWithNoStores)) {
            NewStore();
            daysWithNoStores = 0;
        } else {
            daysWithNoStores++;
        }
    }

    public SavedGame Save() {
        var result = new SavedGame {
            day = day,
            playerItems = player.abilitiesFolder.GetComponentsInDirectChildren<Saver>().Select(s => new SavedItem().Tap(si => {
                si.key = s.key;
            })).ToList()
        };
        return result;
    }

    public void Load(SavedGame savedGame) {
        var items = savedGame.playerItems;
        items.ForEach(item => {
            var itemObject = Library.instance.GetByKey(item.key).Load(null);
            itemObject.transform.SetParent(player.abilitiesFolder);
        });
    }
}
