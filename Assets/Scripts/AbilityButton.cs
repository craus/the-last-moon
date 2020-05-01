﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class AbilityButton : MonoBehaviour
{
    public Ability ability;
    public Button button;
    public ColorBlock baseColors;
    public ColorBlock selectedColors;
    public Color selectedMultiplier;

    public void Click() {
        if (ability.effects.All(e => !e.RequireTarget)) {
            Battle.instance.UseAbility(ability);
        } else {
            BattleUI.instance.currentAbility = ability;
        }
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
            button.interactable = ability.Available;
        }
    }
}
