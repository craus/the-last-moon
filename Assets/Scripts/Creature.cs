using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int damage;
    public int hp;
    public int maxHp;
    public bool Alive => hp > 0;

    public void Hit(int damage = 1) {
        hp -= damage;
        //HpCheck();
        DeathCheck();
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
}
