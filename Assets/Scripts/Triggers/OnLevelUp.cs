using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelUp : Common.Trigger
{
    public Creature creature;

    public void Start() {
        creature.onLevelUp += Run; 
    }

    public void OnDestroy() {
        if (creature != null) {
            creature.onLevelUp -= Run;
        }
    }
}
