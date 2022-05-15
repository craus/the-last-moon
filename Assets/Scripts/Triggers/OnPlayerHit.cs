﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPlayerHit : Common.Trigger
{
    public AbilityEffect source;

    public IntUnityEvent run;

    public void Start() {
        GlobalEvents.instance.onHit += onHit;
    }

    private void onHit(Creature c, int damage, IAttackSource source) {
        if (c is Player) {
            Run(damage);
            Run();
        }
    }

    public void Run(int damage) {
        run.Invoke(damage);
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onHit -= onHit;
        }
    }
}
