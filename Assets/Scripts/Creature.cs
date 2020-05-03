using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    public int damage = 1;
    public int hp = 1;
    public int maxHp = 1;
    public bool maxedHp = true;
    public int stunned = 0;
    public int armor = 0;
    public int regeneration = 0;
    public int protectionUntilEndOfCombat = 0;
    public bool Alive => hp > 0;

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

    public void Hit(int damage = 1) {
        damage = Mathf.Clamp(damage - armor, 0, int.MaxValue);
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
        target.Hit(damage);
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

    private void OnBattleEnd(Battle b) {
        protectionUntilEndOfCombat = 0;
    }
}
