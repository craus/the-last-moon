using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    public int gold;
    public int damage = 1;
    public int hp = 1;
    public int maxHp = 1;
    public bool maxedHp = true;
    public int stunned = 0;
    public int armor = 0;
    public int regeneration = 0;
    public int protectionUntilEndOfCombat = 0;
    public int bubbles = 0;
    public int away = 0;
    public int attack = 0;
    public bool Alive => hp > 0;
    public bool Dead => !Alive;

    public Action<Creature, Creature, int> beforeAttack = (a,b,d) => { };
    public Action<Creature, Creature, int> afterAttack = (a, b, d) => { };

    public List<Buff> buffs = new List<Buff>();

    public bool Targetable => away == 0;

    private void OnValidate() {
        if (maxedHp && maxHp != hp) {
            maxHp = hp;
        }
    }

    public void ApplyProtection(ref int damage, ref int protection) {
        var value = Mathf.Min(damage, protection);
        damage -= value;
        protection -= value;
    }

    public void ApplyBubbles(ref int damage, ref int bubbles) {
        if (damage > 0 && bubbles > 0) {
            damage = 0;
            bubbles--;
        }
    }

    public void Hit(Creature attacker, int damage = 1, AbilityEffect source = null) {
        if (away > 0) {
            return;
        }
        damage += attacker.attack;
        damage = Mathf.Clamp(damage - armor, 0, int.MaxValue);
        ApplyBubbles(ref damage, ref bubbles);
        ApplyProtection(ref damage, ref protectionUntilEndOfCombat);
        LoseHp(damage, source);
    }

    public void LoseHp(int damage = 1, AbilityEffect source = null) {
        hp -= damage;
        DeathCheck(source);
    }

    public void Poison(int poison = 1) {
        regeneration -= poison;
    }

    public void Heal(int heal = 1) {
        hp += heal;
        HpCheck();
        DeathCheck();
    }

    public void Stun(int stun = 1) {
        stunned += stun;
    }

    public void Afterburner(int power = 1) {
        stunned -= power;
    }

    public void HpCheck() {
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    public void DeathCheck(AbilityEffect source = null) {
        if (hp <= 0) {
            Die(source);
        }
    }

    public void Die() {
        Die(null);
    }

    public virtual void Die(AbilityEffect source = null) {
        GlobalEvents.instance.onDeath(this, source);
        Destroy(gameObject);
    }

    public void Attack(Creature target) {
        Attack(target, damage);
    }

    public void Attack(Creature target, int damage) {
        beforeAttack(this, target, damage);
        target.Hit(this, damage);
        afterAttack(this, target, damage);
    }

    public void UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
    }

    public virtual void MakeMove() {
        if (!Battle.On) {
            return;
        }
        if (Dead) {
            return;
        }
        if (stunned > 0) {
            stunned--;
        } else if (away > 0) {
            away--;
        } else {
            TakeAction();
        }
        AfterMove();
    }

    public virtual void AfterMove() {
        Heal(regeneration);
    }

    public virtual void TakeAction() {
    }

    public virtual void Start() {
        GlobalEvents.instance.onBattleEnd += OnBattleEnd;
    }

    public void OnDestroy() {
        if (GlobalEvents.instance) {
            GlobalEvents.instance.onBattleEnd -= OnBattleEnd;
        }
    }

    protected virtual void OnBattleEnd(Battle b) {
        protectionUntilEndOfCombat = 0;
        bubbles = 0;
        away = 0;
        attack = 0;
    }
}
