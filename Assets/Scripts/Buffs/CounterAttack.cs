using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Buff, IAttackModifier, IAttackSource
{
    public int Priority => 100;

    public void ModifyAttack(Attack attack) {
        if (attack.victim == owner) {
            if (attack.damageType != DamageType.Thorns) {
                attack.Does(() => {
                    attack.attacker.Hit(owner, Power, damageType: DamageType.Thorns, source: this);
                });
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

    public void OnOwnerDeath(IAttackSource source) {
        Expire();
    }

    public string Text(Creature user) => "Counterattack";

    public string Description(Creature user) => "Counterattack";

    public override string Name => "Counterattack";
}
