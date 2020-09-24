using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAbilityUse : Common.Trigger
{
    public Ability ability;
    public void Awake() {
        ability.onUse += onUse; 
    }

    private void onUse() {
        DebugManager.LogFormat("onUse");
        Run();
    }

    public void OnDestroy() {
        if (ability != null) {
            ability.onUse -= onUse;
        }
    }
}
