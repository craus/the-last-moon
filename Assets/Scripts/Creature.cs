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

    public void Hit(Creature attacker, int damage = 1) {
        if (away > 0) {
            return;
        }
        damage += attacker.attack;
        damage = Mathf.Clamp(damage - armor, 0, int.MaxValue);
        ApplyBubbles(ref damage, ref bubbles);
        ApplyProtection(ref damage, ref protectionUntilEndOfCombat);
        LoseHp(damage);
    }

    public void LoseHp(int damage = 1) {
        hp -= damage;
        DeathCheck();
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

    public void DeathCheck() {
        if (hp <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        GlobalEvents.instance.onDeath(this);
        Destroy(gameObject);
    }

    public void Attack(Creature target) {
        target.Hit(this, damage);
    }

    public void UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
    }

    public virtual void MakeMove() {
        if (!Alive) {
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

    public void Start() {
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
