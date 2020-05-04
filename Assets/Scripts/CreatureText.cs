using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureText : MonoBehaviour
{
    public Creature creature;
    public TMPro.TextMeshProUGUI text;
    public string format = "{0}/{1}";

    public static string StatusString(Creature creature) {
        return "{regeneration}{protection}{armor}{bubbles}{stunned}".i(new Dictionary<string, object>() {
            { "protection", ProtectionString(creature.protectionUntilEndOfCombat) },
            { "stunned", StunString(creature.stunned) },
            { "armor", creature.armor > 0 ? "A{0}".i(creature.armor) : creature.armor < 0 ? "V{0}".i(-creature.armor) : "" },
            { "bubbles", creature.bubbles > 0 ? "()".repeat(creature.bubbles) : "" },
            { "regeneration", RegenerationString(creature.regeneration) },
        });
    }

    public static string StunString(int s, string c = "*") {
        return s < 0 ? StunString(-s, "+") : s < 4 ? c.repeat(s) : c + "{0}".i(s);
    }

    public static string RegenerationString(int v) {
        return v < 0 ? "<color=#00a000ff>P{0}</color>".i(-v) : v > 0 ? "<color=#40b0b0ff>R{0}</color>".i(v) : "";
    }

    public static string ProtectionString(int v) {
        return v > 0 ? "<color=#808080ff>p{0}</color>".i(v) : "";
    }

    public static string FormatCreature(string format, Creature creature) {
        return format.i(new Dictionary<string, object>() {
            { "away", "<".repeat(creature.away) },
            { "damage", creature.damage },
            { "hp", creature.hp },
            { "maxHp", creature.maxHp },
            { "protection", ProtectionString(creature.protectionUntilEndOfCombat) },
            { "hpStatus", creature.Alive ? "{0}/{1}".i(creature.hp, creature.maxHp) : "DEAD" },
            { "stunned", StunString(creature.stunned) },
            { "armor", creature.armor > 0 ? "A{0}".i(creature.armor) : creature.armor < 0 ? "V{0}".i(-creature.armor) : "" },
            { "bubble", "()".repeat(creature.bubbles) },
            { "regeneration", RegenerationString(creature.regeneration) },
            { "status", StatusString(creature) },
        });
    }

    public void Update() {
        text.text = FormatCreature(format, creature);
    }
}
