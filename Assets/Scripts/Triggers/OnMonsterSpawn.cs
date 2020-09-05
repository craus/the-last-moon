using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMonsterSpawn : Common.Trigger
{
    public CreatureEvent activateOnCreature;

    public void Start() {
        GlobalEvents.instance.onSpawn += OnSpawn; 
    }

    private void OnSpawn(Creature c) {
        if (c is Monster) {
            Run();
            Run(c);
        }
    }

    public void Run(Creature c) {
        activateOnCreature.Invoke(c);
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onSpawn -= OnSpawn;
        }
    }
}
