using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBattleStart : Common.Trigger
{
    public void Awake() {
        GlobalEvents.instance.onBattleStart += onBattleStart; 
    }

    private void onBattleStart(Battle b) {
        Run();
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onBattleStart -= onBattleStart;
        }
    }
}
