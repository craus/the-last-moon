using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Samples
// PreventNextDamageDealtTo
// AddConstantToDamage

/// <summary>
/// Modifies creature attack
/// </summary>
public class Attack
{
    public Creature attacker;
    public Creature victim;

    public int damage;
    public DamageType damageType;

    public AbilityEffect source;

    public bool interrupted = false;

    public List<DescriptedAction> consequences = new List<DescriptedAction>();

    public Attack Does(Action a) {
        consequences.Add(new DescriptedAction(a));
        return this;
    }

    public Attack(
        Creature attacker, 
        Creature victim, 
        int damage = 1, 
        AbilityEffect source = null, 
        DamageType damageType = DamageType.Default
    ) {
        this.attacker = attacker;
        this.victim = victim;
        this.damage = damage;
        this.source = source;
        this.damageType = damageType;
    }

    public Attack ApplyBuffs() {
        IEnumerable<IAttackModifier> attackModifiers =
            (attacker?.buffs?.Where(b => b is IAttackModifier) ?? CollectionExtensions.Empty<Buff>())
            .Concat(victim?.buffs?.Where(b => b is IAttackModifier))
            .Cast<IAttackModifier>()
            .Unique();

        foreach (var am in attackModifiers.OrderBy(am => am.Priority)) {
            am.ModifyAttack(this);
            if (interrupted) {
                break;
            }
        }
        return this;
    }

    public Attack Execute() {
        consequences.ForEach(a => a.action());
        return this;
    }
}
