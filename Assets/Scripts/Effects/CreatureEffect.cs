using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureEffect : Common.Effect
{
    [SerializeField] private Creature creature;
    [SerializeField] private CreatureProvider creatureProvider;

    public Creature Creature => creatureProvider != null ? creatureProvider.Value : creature;

    public virtual void Run(Creature creature) {
    }
}
