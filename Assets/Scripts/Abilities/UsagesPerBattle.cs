using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagesPerBattle : NonTargetEffect
{
    public override bool AllowUsage => usages >= 1;

    public int usages;
    public int usagesPerBattle;
    public Ability ability;

    public override void Use(Creature user) {
        usages -= 1;
    }

    public override string Text(Creature user) {
        return " ({0}/{1}R)".i(usages, usagesPerBattle);
    }

    public void Start() {
        GlobalEvents.instance.onBattleStart += OnBattleStart;
        GlobalEvents.instance.onBattleEnd += OnBattleEnd;
    }

    public void OnDestroy() {
        if (GlobalEvents.instance) {
            GlobalEvents.instance.onBattleStart -= OnBattleStart;
            GlobalEvents.instance.onBattleEnd -= OnBattleEnd;
        }
    }

    private void OnBattleStart(Battle b) {
        usages = usagesPerBattle;
    }

    private void OnBattleEnd(Battle b) {
        usages = usagesPerBattle;
    }
}
