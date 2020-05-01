using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureText : MonoBehaviour
{
    public Creature creature;
    public TMPro.TextMeshProUGUI text;
    public string format = "{0}/{1}";

    public void Update() {
        text.text = format.i(new Dictionary<string, object>() {
            { "damage", creature.damage },
            { "hp", creature.hp },
            { "maxHp", creature.maxHp },
            { "hpStatus", creature.Alive ? "{0}/{1}".i(creature.hp, creature.maxHp) : "DEAD" },
            { "stunned", creature.stunned < 4 ? "*".repeat(creature.stunned) : "*{0}".i(creature.stunned) },
        });
    }
}
