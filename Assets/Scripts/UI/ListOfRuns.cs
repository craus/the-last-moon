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
        var profile = Statistics.CurrentProfile;
        profile.runs.OrderBy(r => (-r.Day)).ForEach(r => CreateGameRunScript(r, profile));

    }

    private void CreateGameRunScript(GameRun run, PlayerProfile profile) {
        var runScript = Instantiate(gameRunScriptSample, transform);
        runScript.gameRun = run;
        runScript.playerProfile = profile;
    }
}
