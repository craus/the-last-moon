using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProfile
{
    public string name = "Shlakoblock";

    public List<GameRun> runs = new List<GameRun>();

    public GameRun currentRun;

    public SavedGame savedGame;

    public void UpdateVersion() {
    }
}
