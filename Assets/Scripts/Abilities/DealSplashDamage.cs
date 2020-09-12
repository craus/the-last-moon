using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealSplashDamage : NonTargetEffect
{
    public int damage;

    public bool enemiesOnly = true;

    public IEnumerable<Creature> Targets => enemiesOnly ? FindObjectsOfType<Monster>() : FindObjectsOfType<Creature>();

    public override void Use(Creature user) {
        Targets.ForEach(m => m.Hit(user, damage, this));
    }

    public override string Text(Creature user) {
        return (enemiesOnly ? "S" : "<color=#ff0000ff>S</color>") + (damage).ToString();
    }

    public override string Description(Creature user) {
        return $"Deal {damage} damage to all {(enemiesOnly ? "enemies" : "creatures")} in battle";
    }
}
