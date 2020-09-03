using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameRun
{
    public int deathDay;

    public GameRun(int day) {
        deathDay = day;
    }

    public string Text() {
        return $"Killed on day {deathDay}";
    }
}
