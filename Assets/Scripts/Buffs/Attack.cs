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
}
