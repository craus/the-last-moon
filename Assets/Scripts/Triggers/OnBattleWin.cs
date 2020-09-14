using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBattleWin : Common.Trigger
{
    public Battle battle;

    public void Start() {
        battle.onWin += OnWin; 
    }

    private void OnWin() {
        Run();
    }

    public void OnDestroy() {
        if (battle != null) {
            battle.onWin -= OnWin;
        }
    }
}
