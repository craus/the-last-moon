using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMonsterLoseHpBy : MonoBehaviour
{
    public AbilityEffect source;

    public IntUnityEvent run;

    public void Start() {
        GlobalEvents.instance.onLoseHp += onLoseHp;
    }

    private void onLoseHp(Creature c, int damage, AbilityEffect source) {
        if (c is Monster && source == this.source) {
            Run(damage);
        }
    }

    public void Run(int damage) {
        run.Invoke(damage);
    }

    public void OnDestroy() {
        if (GlobalEvents.instance != null) {
            GlobalEvents.instance.onLoseHp -= onLoseHp;
        }
    }
}
