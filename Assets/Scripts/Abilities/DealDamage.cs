using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : AbilityEffect
{
    public int damage;
    public IntValueProvider damageProvider;
    public int Damage => damageProvider != null ? damageProvider.Value : damage;

    public override void Use(Creature user, Creature target) {
        target.Hit(Damage);
    }

    public override string Text() {
        return Damage.ToString();
    }
}
