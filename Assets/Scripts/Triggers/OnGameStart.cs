using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStart : Common.Trigger
{
    public void Awake() {
        GlobalEvents.instance.onGameStart += onGameStart; 
    }

    private void onGameStart(Game g) {
        Run();
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onGameStart -= onGameStart;
        }
    }
}
