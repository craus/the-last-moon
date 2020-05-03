﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class AbilityText : MonoBehaviour
{
    public Ability ability;
    public TMPro.TextMeshProUGUI text;
    public string format = "{damage}{splash}{stun}";

    public void Update() {
        text.text = string.Join("", ability.effects.Select(e => e.Text()));
    }
}
