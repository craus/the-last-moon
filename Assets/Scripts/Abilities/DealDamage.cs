using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DealDamage : AbilityEffect
{
    public int damage;
    public IntValueProvider damageProvider;
    public int Damage => damageProvider != null ? damageProvider.Value : damage;
    public int DamageWithBuffs(Creature user) => user == null ? Damage : new Attack(user, null, Damage, this).ApplyBuffs().damage;

    public override void Use(Creature user, Creature target, Ability ability) {
        target.Hit(user, Damage, this, ability);
    }

    private string DamageText(Creature user) {
        if (DamageWithBuffs(user) > Damage) {
            return $"<color=green>{DamageWithBuffs(user)}</color>";
        }
        if (DamageWithBuffs(user) < Damage) {
            return $"<color=red>{DamageWithBuffs(user)}</color>";
        }
        return $"{Damage}";
    }

    public override string Text(Creature user) {
        return DamageText(user);
    }

    public override string Description(Creature user) {
        return $"Deal {DamageText(user)} damage";
    }
}
