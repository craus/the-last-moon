using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public int damage;
    public int stun;
    public int heal;
    public int splashDamage;

    public void Use(Creature user, Creature target) {
        target.Hit(damage);
        target.Stun(stun);
        target.Heal(heal);
        FindObjectsOfType<Monster>().ForEach(m => m.Hit(splashDamage));
    }
}
