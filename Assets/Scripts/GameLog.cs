using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLog : Singletone<GameLog>
{
    public TMPro.TextMeshProUGUI text;

    private static int messageID = 0;

    public static void Message(string text, params object[] args) {
        ++messageID;

        string message = "{0} {1}".i(messageID, text.i(args));

        instance.Message(message);

        DebugManager.LogFormat(text, args);
    }

    public void Message(string message) {
        text.text += message + "\n";
    }

    public static void LogMonsters() {
        DebugManager.LogFormat("Monsters: {0}", FindObjectsOfType<Monster>().ExtToString(
            elementToString: m => m.Text(),
            format: "{0}"
        ));
        GameLog.Message(
            "Monsters: {0}".i(
                FindObjectsOfType<Monster>()
                    .Where(m => !(m is MonsterSpawner))
                    .ExtToString(
                        elementToString: m => m.Text(),
                        format: "{0}"
                    )
            )
        );
    }

    public static void LogBattleRound() {
        Message($"Battle round {Battle.instance.moveNumber}");
    }
}
