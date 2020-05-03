using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public Battle battleSample;
    public Game gameSample;
    public Game game;

    public void Awake() {
        game = FindObjectOfType<Game>();
    }

    public void Update() {
        CheckButtons();
    }

    public void CheckButtons() {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift)) {
            RestartGame();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartBattle();
            return;
        }
    }

    public void DestroyBattle() {
        var battle = FindObjectOfType<Battle>();
        if (battle != null) {
            Destroy(battle.gameObject);
        }
    }

    public void DestroyGame() {
        if (game != null) {
            Destroy(game.gameObject);
        }
    }

    public void NewBattle() {
        Instantiate(battleSample, game.transform);
    }

    public void NewGame() {
        game = Instantiate(gameSample);
    }

    public void RestartBattle() {
        DestroyBattle();
        NewBattle();
    }

    public void RestartGame() {
        DestroyGame();
        NewGame();
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
        if (ability.effects.All(e => !e.RequireTarget)) {
            PlayerUseAbility(ability);
        } else {
            AbilitiesController.instance.currentAbility = ability;
        }
    }

    public void PlayerUseAbility(Ability a, Creature t = null) {
        if (Battle.instance != null) {
            Game.instance.player.MakeMove(a, t);
        } else {
            Game.instance.player.UseAbility(a, t);
        }
    }
}
