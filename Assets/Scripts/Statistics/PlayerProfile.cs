using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void UpdateRunRanks() {
        var list = runs.OrderBy(r => -r.Score).ToList();
        for (int i = 0; i < list.Count; i++) {
            list[i].rank = i + 1;
        }
    }
}
