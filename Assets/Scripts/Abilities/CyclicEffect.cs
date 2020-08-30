using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CyclicEffect : AbilityEffect
{
    public override bool RequireTarget => current.RequireTarget;

    public List<AbilityEffect> effects;
    public AbilityEffect current;

    public override void Use(Creature user, Creature target) {
        current.Use(user, target);
        current = effects.CyclicNext(current);
    }

    public override string Text(Creature user) {
        return effects.Select(e2 => (current == e2 ? "<b>{0}</b>" : "{0}").i(e2.Text(user))).Join("/");
    }

    public override string Description(Creature user) {
        return effects
            .Select((e2, index) => (current == e2 ? "<b>{0}</b>" : "{0}").i($"Phase {index+1}: {e2.Description(user)}"))
            .Join("\n\n");
    }

    public void Start() {
        GlobalEvents.instance.onBattleStart += OnBattleStart;
        Reset();
    }

    public void OnDestroy() {
        if (GlobalEvents.instance) {
            GlobalEvents.instance.onBattleStart -= OnBattleStart;
        }
    }

    private void Reset() {
        current = effects[0];
    }

    private void OnBattleStart(Battle battle) {
        Reset();
    }
}
