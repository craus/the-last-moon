using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : Common.Trigger
{
    public Creature creature;

    public void Start() {
        creature.onDeath += OnDeathHandler; 
    }

    private void OnDeathHandler(IAttackSource source) {
        Run();
    }

    public void OnDestroy() {
        if (creature != null) {
            creature.onDeath -= OnDeathHandler;
        }
    }
}
