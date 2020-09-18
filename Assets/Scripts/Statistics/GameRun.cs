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

    public int day;
    public Status status;

    public GameRun(int day = 0, Status status = Status.Alive) {
        this.day = day;
        this.status = status;
    }

    public void Abandon() {
        if (status != Status.Alive) {
            return;
        }
        status = Status.Interrupted;
    }

    public string Text() {
        return $"{status} on day {day}";
    }
}
