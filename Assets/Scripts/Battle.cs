using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public static bool On => instance != null;

    public int moveNumber;

    public void Finish() {
        Destroy(gameObject);
        AbilitiesController.instance.currentAbility = null;
        GlobalEvents.instance.onBattleEnd(this);
    }

    public void Start() {
        GlobalEvents.instance.onBattleStart(this);
    }
}
