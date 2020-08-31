using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventAbilityEffect : NonTargetEffect
{
    public UnityEvent use;

    [SerializeField] private bool battleOnly = true;

    public override bool BattleOnly => battleOnly;

    public override void Use(Creature user) {
        use.Invoke();
    }
}
