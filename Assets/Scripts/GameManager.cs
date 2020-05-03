using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
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
            game.RestartBattle();
            return;
        }
    }

    public void DestroyGame() {
        if (game != null) {
            Destroy(game.gameObject);
        }
    }

    public void NewGame() {
        game = Instantiate(gameSample);
    }

    public void RestartGame() {
        DestroyGame();
        NewGame();
    }
}
