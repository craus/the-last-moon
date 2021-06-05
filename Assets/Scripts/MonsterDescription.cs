using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class MonsterDescription : MonoBehaviour
{
    public Monster monster;
    public TMPro.TextMeshProUGUI text;

    public void Update() {
        text.text = $"<b>{monster.name}</b>";
    }
}
