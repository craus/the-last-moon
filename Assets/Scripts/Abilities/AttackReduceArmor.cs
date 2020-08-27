using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReduceArmor : Buff
{
    public Creature creature;

    public void Awake() {
        creature = GetComponent<Creature>();
    }

    public void Start() {
        creature.afterAttack += CreatureAfterAttack;
    }

    public void OnDestroy() {
        creature.afterAttack -= CreatureAfterAttack;
    }

    public void CreatureAfterAttack(Creature attacker, Creature target, int damage) {
        target.armor -= power;
    }
}
