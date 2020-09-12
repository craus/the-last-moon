using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : CreatureEffect
{
    [SerializeField] private int power;
    public IntValueProvider powerProvider;

    public int Power => powerProvider != null ? powerProvider.Value : power;

    public override void Run(Creature creature) {
        creature.ApplyBuff<Stunned>(power);
    }

    public override void Run() {
        Creature.ApplyBuff<Stunned>(power);
    }
}
