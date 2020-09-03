using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfRuns : MonoBehaviour
{
    public GameRunScript gameRunScriptSample;

    public void UpdateList() {
        transform.Children().ForEach(c => Destroy(c.gameObject));
        Statistics.Load().playerProfiles[0].runs.ForEach(CreateGameRunScript);
    }

    private void CreateGameRunScript(GameRun run) {
        var runScript = Instantiate(gameRunScriptSample, transform);
        runScript.gameRun = run;
    }
}
