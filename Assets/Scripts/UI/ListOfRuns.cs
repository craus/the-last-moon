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
        //(double, int) t = (4.5, 3);
        Statistics.Runs.OrderBy(r => (r.status == GameRun.Status.Alive ? 0 : 1, r.day)).ForEach(CreateGameRunScript);
    }

    private void CreateGameRunScript(GameRun run) {
        var runScript = Instantiate(gameRunScriptSample, transform);
        runScript.gameRun = run;
    }
}
