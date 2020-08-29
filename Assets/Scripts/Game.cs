﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singletone<Game>
{
    public int moveNumber;

    public Battle battleSample;
    public Player player;

    public void DestroyBattle() {
        var battle = FindObjectOfType<Battle>();
        if (battle != null) {
            Destroy(battle.gameObject);
        }
    }

    public void NewBattle() {
        moveNumber++;
        var battle = Instantiate(battleSample, transform);
        battle.GetComponentInChildren<MonsterSpawner>().mana += moveNumber;
    }

    public void RestartBattle() {
        if (Battle.instance != null) {
            Battle.instance.Finish();
        }
        NewBattle();
    }

    public void Click(Creature creature) {
        if (AbilitiesController.instance.currentAbility != null) {
            PlayerUseAbility(AbilitiesController.instance.currentAbility, creature);
        }
    }

    public void ClickAbility(Ability ability) {
        if (Battle.instance == null && ability.BattleOnly) {
            return;
        }
        if (!ability.RequireTarget) {
            PlayerUseAbility(ability);
        } else {
            AbilitiesController.instance.currentAbility = ability;
        }
    }

    public void PlayerUseAbility(Ability a, Creature t = null) {
        if (Battle.instance != null) {
            player.MakeMove(a, t);
        } else {
            player.UseAbility(a, t);
        }
    }

    public void Start() {
        GlobalEvents.instance.onGameStart.Invoke(this);
    }
}
