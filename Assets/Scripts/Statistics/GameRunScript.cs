using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRunScript : MonoBehaviour
{
    public GameRun gameRun;
    public PlayerProfile playerProfile;

    public TMPro.TextMeshProUGUI description;
    public TMPro.TextMeshProUGUI items;
    public TMPro.TextMeshProUGUI rank;

    public Image back;
    public Image highlightedBack;

    bool Current => gameRun == playerProfile.currentRun;

    public void Start() {
        description.text = gameRun.Text();
        items.text = gameRun.savedGame.playerItems.ExtToString(
            format: "{0}",
            elementToString: si => Library.instance.GetByKey(si.key).GetComponent<GenericAbility>().Text(null)
        );
        rank.text = $"#{gameRun.rank}";
        back.enabled = !Current;
        highlightedBack.enabled = Current;
    }
}
