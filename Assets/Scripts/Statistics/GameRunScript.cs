using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunScript : MonoBehaviour
{
    public GameRun gameRun;

    public TMPro.TextMeshProUGUI description;
    public TMPro.TextMeshProUGUI items;

    public void Start() {
        description.text = gameRun.Text();
        items.text = gameRun.savedGame.playerItems.ExtToString(
            format: "{0}",
            elementToString: si => Library.instance.GetByKey(si.key).GetComponent<GenericAbility>().Text(null)
        );
    }
}
