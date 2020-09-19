using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameRun
{
    public enum Status
    {
        Undefined = 0,
        Default = 10, 
        Alive = 20,
        Dead = 30,
        Interrupted = 40
    }

    public Status status;
    public SavedGame savedGame;

    public int Day => savedGame.day;

    public int rank;

    public int Score => Day;

    public GameRun(SavedGame savedGame, Status status = Status.Alive) {
        this.status = status;
        this.savedGame = savedGame;
    }

    public void Abandon() {
        if (status != Status.Alive) {
            return;
        }
        status = Status.Interrupted;
    }

    public string Text() {
        return $"{status} on day {Day}";
    }
}
