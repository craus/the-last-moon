using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunScript : MonoBehaviour
{
    public GameRun gameRun;

    public TMPro.TextMeshProUGUI description;

    public void Start() {
        description.text = gameRun.Text();
    }
}
