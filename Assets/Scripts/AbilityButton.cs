﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class AbilityButton : MonoBehaviour
{
    public Ability ability;
    public Button button;
    public ColorBlock baseColors;
    public ColorBlock selectedColors;
    public Color selectedMultiplier;

    public void Click() {
        BattleUI.instance.currentAbility = ability;
    }

    [ContextMenu("Reset Colors")]
    public void ResetColors() {
        baseColors = button.colors;
        selectedColors = button.colors.Multiplied(selectedMultiplier);
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void Update() {
        if (!Extensions.InEditMode()) {
            button.colors = BattleUI.instance.currentAbility == ability ? selectedColors : baseColors;
        }
    }
}
