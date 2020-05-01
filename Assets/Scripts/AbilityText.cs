using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AbilityText : MonoBehaviour
{
    public Ability ability;
    public TMPro.TextMeshProUGUI text;
    public string format = "{damage}{splash}{stun}";

    public void Update() {
        text.text = format.i(new Dictionary<string, object>() {
            { "damage", ability.damage > 0 ? "{0}".i(ability.damage) : "" },
            { "splash", ability.splashDamage > 0 ? "S{0}".i(ability.splashDamage) : "" },
            { "stun", "*".repeat(ability.stun) },
            { "heal", ability.heal > 0 ? "H{0}".i(ability.heal) : "" },
        });
    }
}
