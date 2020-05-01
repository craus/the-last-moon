using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int damage;
    public int hp;
    public int maxHp;
    public bool maxedHp;
    public int stunned;
    public bool Alive => hp > 0;

    private void OnValidate() {
        if (maxedHp) {
            maxHp = hp;
        }
    }

    public void Hit(int damage = 1) {
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
