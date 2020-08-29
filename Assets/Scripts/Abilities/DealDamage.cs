using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : AbilityEffect
{
    public int damage;
    public IntValueProvider damageProvider;
    public int Damage => damageProvider != null ? damageProvider.Value : damage;

    public override void Use(Creature user, Creature target) {
        target.Hit(user, Damage, this);
    }

    public override string Text(Creature user) {
        return (Damage).ToString();
    }

    public override string Description(Creature user) {
        return $"Deal {damage} damage";
    }
}
