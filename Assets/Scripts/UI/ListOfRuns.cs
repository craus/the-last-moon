using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ListOfRuns : MonoBehaviour
{
    public GameRunScript gameRunScriptSample;

    public void UpdateList() {
        transform.Children().ForEach(c => Destroy(c.gameObject));
        Statistics.UpdateCurrentProfile(p => p.UpdateRunRanks());
        Statistics.Runs.OrderBy(r => (-r.Day)).ForEach(CreateGameRunScript);

    }

    private void CreateGameRunScript(GameRun run) {
        var runScript = Instantiate(gameRunScriptSample, transform);
        runScript.gameRun = run;
    }
}
