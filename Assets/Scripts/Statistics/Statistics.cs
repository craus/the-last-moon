using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public const string SAVE_FILE_NAME = "moon.sav";

    public static SaveFileState Load() {
        var result = FileManager.LoadFromFile<SaveFileState>(SAVE_FILE_NAME);
        if (result == null) {
            DebugManager.LogFormat("No save file detected; creating new save file");
            result = new SaveFileState();
            Save(result);
        }
        return result;
    }

    public static void Save(SaveFileState s) {
        FileManager.SaveToFile(s, SAVE_FILE_NAME);
    }

    public static void RegisterRun(GameRun run) {
        Save(Load().Tap(s => s.playerProfiles[0].runs.Add(run)));
    }
}
