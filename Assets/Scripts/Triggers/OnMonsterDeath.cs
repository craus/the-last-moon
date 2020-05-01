using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMonsterDeath : Common.Trigger
{
    public void Start() {
        GlobalEvents.instance.onDeath += OnDeath; 
    }

    private void OnDeath(Creature c) {
        if (c is Monster) {
            Run();
        }
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onDeath -= OnDeath;
        }
    }
}
