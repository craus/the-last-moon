using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class BattleText : MonoBehaviour
{
    public Battle battle;
    public TMPro.TextMeshProUGUI text;
    public string format = "{moveNumber}";

    public void Update() {
        text.text = format.i(new Dictionary<string, object>() {
            { "moveNumber", battle.moveNumber },
        });
    }
}
