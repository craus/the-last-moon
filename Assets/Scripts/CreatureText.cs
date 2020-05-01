using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureText : MonoBehaviour
{
    public Creature creature;
    public TMPro.TextMeshProUGUI text;
    public string format = "{0}/{1}";

    public string StunString(int s, string c = "*") {
        return s < 0 ? StunString(-s, "+") : s < 4 ? c.repeat(s) : c + "{0}".i(s);
    }

    public void Update() {
        text.text = format.i(new Dictionary<string, object>() {
            { "damage", creature.damage },
            { "hp", creature.hp },
            { "maxHp", creature.maxHp },
            { "hpStatus", creature.Alive ? "{0}/{1}".i(creature.hp, creature.maxHp) : "DEAD" },
            { "stunned", StunString(creature.stunned) },
            { "armor", creature.armor > 0 ? "A{0}".i(creature.armor) : creature.armor < 0 ? "V{0}".i(-creature.armor) : "" },
        });
    }
}
