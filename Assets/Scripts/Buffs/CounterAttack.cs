using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Buff, IAttackModifier
{
    public int Priority => 100;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            if (attack.damageType != DamageType.Thorns) {
                attack.attacker.Hit(owner, power, damageType: DamageType.Thorns);
            }
        }
    }

    public override Creature owner { 
        get => base.owner; 
        set {
            if (base.owner != null) {
                base.owner.onDeath -= OnOwnerDeath;
            }
            base.owner = value; 
            if (base.owner != null) {
                base.owner.onDeath += OnOwnerDeath;
            }
        }
    }

    public void OnOwnerDeath(AbilityEffect source) {
        Expire();
    }

    public override string Name => "Counterattack";
}
