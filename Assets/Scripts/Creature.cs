using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int damage = 1;
    public int hp = 1;
    public int maxHp = 1;
    public bool maxedHp = true;
    public int stunned = 0;
    public int armor = 0;
    public bool Alive => hp > 0;

    private void OnValidate() {
        if (maxedHp) {
            maxHp = hp;
        }
    }

    public void Hit(int damage = 1) {
        damage = Mathf.Clamp(damage - armor, 0, int.MaxValue);
        hp -= damage;
        DeathCheck();
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
        Destroy(gameObject);
    }

    public void Attack(Creature target) {
        target.Hit(damage);
    }

    public void UseAbility(Ability ability, Creature target) {
        ability.Use(this, target);
    }

    public void MakeMove() {
        if (!Alive) {
            return;
        }
        if (stunned > 0) {
            stunned--;
            return;
        }
        TakeAction();
    }

    public virtual void TakeAction() {
    }
}
