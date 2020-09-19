using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public const string SAVE_FILE_NAME = "moon.sav";

    public static PlayerProfile CurrentProfile => Load().currentProfile;
    public static IEnumerable<GameRun> Runs => CurrentProfile.runs;

    public static void WipeSave() {
        FileManager.RemoveFile(SAVE_FILE_NAME);
        Save(new SaveFileState());
    }

    public static SaveFileState Load() {
        var result = FileManager.LoadFromFile<SaveFileState>(SAVE_FILE_NAME);
        if (result == null) {
            DebugManager.LogFormat("No save file detected; creating new save file");
            result = new SaveFileState();
            Save(result);
        }
        result.UpdateVersion();
        return result;
    }

    public static void Save(SaveFileState s) {
        FileManager.SaveToFile(s, SAVE_FILE_NAME);
    }

    public static void RegisterFinishedRun(GameRun run) {
        Save(Load().Tap(s => s.playerProfiles[0].runs.Add(run)));
    }

    public static void RegisterDeath() {
        UpdateCurrentRun(r => r.status = GameRun.Status.Dead);
    }

    public static void RegisterCurrentDay(int day) {
        UpdateCurrentRun(r => r.day = day);
    }

    public static void RegisterNewRun() {
        UpdateCurrentProfile(p => {
            var run = new GameRun();
            p.runs.Add(run);
            p.currentRun?.Abandon();
            p.currentRun = run;
        });
    }

    public static void UpdateCurrentProfile(Action<PlayerProfile> update) {
        Save(Load().Tap(s => update(s.currentProfile)));
    }

    public static void UpdateCurrentRun(Action<GameRun> update) {
        UpdateCurrentProfile(p => update(p.currentRun));
    }
}
