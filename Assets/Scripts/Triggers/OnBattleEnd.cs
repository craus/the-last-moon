using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBattleEnd : Common.Trigger
{
    public void Start() {
        GlobalEvents.instance.onBattleEnd += onBattleEnd; 
    }

    private void onBattleEnd(Battle b) {
        Run();
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onBattleEnd -= onBattleEnd;
        }
    }
}
