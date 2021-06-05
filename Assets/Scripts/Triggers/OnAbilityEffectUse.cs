using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAbilityEffectUse : Common.Trigger
{
    public AbilityEffect effect;
    public void Awake() {
        effect.onUse += onUse; 
    }

    private void onUse() {
        DebugManager.LogFormat($"onUse {effect}");
        Run();
    }

    public void OnDestroy() {
        if (effect != null) {
            effect.onUse -= onUse;
        }
    }
}
