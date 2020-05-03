using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public Battle battleSample;
    public Player player;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }

    public void DestroyBattle() {
        var battle = FindObjectOfType<Battle>();
        if (battle != null) {
            Destroy(battle.gameObject);
        }
    }

    public void NewBattle() {
        Instantiate(battleSample);
    }

    public void Restart() {
        DestroyBattle();
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
        if (ability.effects.All(e => !e.RequireTarget)) {
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
}
