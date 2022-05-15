using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMonsterDeathBy : Common.Trigger
{
    public AbilityEffect source;

    public void Start() {
        GlobalEvents.instance.onDeath += OnDeath; 
    }

    private void OnDeath(Creature c, IAttackSource source) {
        if (c is Monster && source == this.source) {
            Run();
        }
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onDeath -= OnDeath;
        }
    }
}
