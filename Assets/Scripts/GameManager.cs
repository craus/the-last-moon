using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    private Queue<Action> plannedActions = new Queue<Action>();

    private IPromise currentProcess;

    private void InitializeProcessQueue() {
        currentProcess = Promise.Resolved();
    }

    public void PlanProcess(Func<IPromise> process) {
        currentProcess = currentProcess.Then(process);
    }

    public void PlanInstantProcess(Action process) {
        currentProcess = currentProcess.Then(() => {
            process();
            return Promise.Resolved();
        });
    }

    public void ExecutePlannedActions() {
        for (int i = 0; i < 10000 && plannedActions.Count() > 0; i++) {
            plannedActions.Peek().Invoke();
            plannedActions.Dequeue();
        }
    }

    public void PlanAction(Action a) {
        plannedActions.Enqueue(a);
    }

    public Game gameSample;
    public Game game;

    public void Awake() {
        //UnityEngine.Random.InitState(42);
        InitializeProcessQueue();

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
