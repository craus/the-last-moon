﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureText : MonoBehaviour
{
    public Creature creature;
    public TMPro.TextMeshProUGUI text;
    public string format = "{0}/{1}";

    public static string StatusString(Creature creature) {
        return "{regeneration}{protection}{armor}{counterattack}{bubbles}{stunned}{slow}{buffs}".i(new Dictionary<string, object>() {
            { "protection", ProtectionString(creature.buffPower<Protection>()) },
            { "stunned", StunString(creature.buffPower<Stunned>()) },
            { "slow", SlowString(creature.buffPower<Slow>()) },
            { "armor", creature.buffPower<Armor>() > 0 ? "A{0}".i(creature.buffPower<Armor>()) : creature.buffPower<Armor>() < 0 ? "V{0}".i(-creature.buffPower<Armor>()) : "" },
            { "counterattack", creature.buff<CounterAttack>() != null ? "C{0}".i(creature.buffPower<CounterAttack>()) : "" },
            { "bubbles", creature.buffPower<Bubble>() > 0 ? "()".repeat(creature.buffPower<Bubble>()) : "" },
            { "regeneration", RegenerationString(creature.buffPower<Regeneration>()) },
            { "buffs", creature.buffs.Select(b => b.Text()).Join() },
        });
    }

    public static string StunString(int s, string c = "*") {
        return s < 0 ? StunString(-s, "+") : s < 4 ? c.repeat(s) : c + "{0}".i(s);
    }

    public static string SlowString(int s, string c = "_") {
        return c.repeat(s);
    }

    public static string RegenerationString(int v) {
        return v < 0 ? "<color=#00a000ff>P{0}</color>".i(-v) : v > 0 ? "<color=#40b0b0ff>R{0}</color>".i(v) : "";
    }

    public static string AttackString(string s, int v) {
        return v != 0 ? $"<color=#ff8080ff>{s}{(v > 0 ? "+" : "")}{v}</color>" : "";
    }

    public static string ProtectionString(int v) {
        return v > 0 ? "<color=#808080ff>p{0}</color>".i(v) : "";
    }

    public static string FormatCreature(string format, Creature creature) {
        return format.i(new Dictionary<string, object>() {
            { "gold", "${0}".i(creature.gold) },
            { "experience", "{0}".i(creature.Experience) },
            { "experienceForLevel", "{0}".i(creature.NextLevelCost) },
            { "level", "{0}".i(creature.level) },
            { "skillPoints", "{0}".i(creature.skillPoints) },
            { "away", "<".repeat(creature.buffPower<Away>()) },
            { "damage", creature.damage + creature.buffPower<IncreasedAttack>() },
            { "attack", AttackString("a", creature.buffPower<IncreasedAttack>()) },
            { "hp", creature.hp },
            { "maxHp", creature.maxHp },
            { "protection", ProtectionString(creature.buffPower<Protection>()) },
            { "hpStatus", creature.Alive ? "{0}/{1}".i(creature.hp, creature.maxHp) : "DEAD" },
            { "stunned", StunString(creature.buffPower<Stunned>()) },
            { "slow", SlowString(creature.buffPower<Slow>()) },
            { "armor", creature.buffPower<Armor>() > 0 ? "A{0}".i(creature.buffPower<Armor>()) : creature.buffPower<Armor>() < 0 ? "V{0}".i(-creature.buffPower<Armor>()) : "" },
            { "counterattack", creature.buff<CounterAttack>() != null ? "C{0}".i(creature.buffPower<CounterAttack>()) : "" },
            { "bubble", "()".repeat(creature.buffPower<Bubble>()) },
            { "regeneration", RegenerationString(creature.buffPower<Regeneration>()) },
            { "status", StatusString(creature) },
        });
    }

    public void Update() {
        var t = FormatCreature(format, creature);
        if (text.text != t) {
            text.text = t;
        }
    }
}
