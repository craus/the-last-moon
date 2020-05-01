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

    public string EffectText(AbilityEffect e) {
        if (e is DealDamage) {
            return (e as DealDamage).damage.ToString();
        }
        if (e is DealSplashDamage) {
            return "S" + (e as DealSplashDamage).damage.ToString();
        }
        if (e is Heal) {
            return "H" + (e as Heal).heal.ToString();
        }
        if (e is SelfHeal) {
            return "h" + (e as SelfHeal).heal.ToString();
        }
        if (e is Stun) {
            return CreatureText.StunString((e as Stun).stun);
        }
        if (e is Afterburner) {
            return CreatureText.StunString(-(e as Afterburner).power);
        }
        if (e is Spend) {
            return " ({0})".i((int)(e as Spend).usages);
        }
        if (e is CyclicEffect) {
            return (e as CyclicEffect).effects.Select(e2 => ((e as CyclicEffect).current == e2 ? "<b>{0}</b>" : "{0}").i(EffectText(e2))).Join("/");
        }
        if (e is SelfDamage) {
            return "X" + (e as SelfDamage).damage;
        }
        if (e is ApplyPoison) {
            return CreatureText.RegenerationString(-(e as ApplyPoison).poison);
        }
        return "?";
    }

    public void Update() {
        text.text = string.Join("", ability.effects.Select(EffectText));
    }
}
