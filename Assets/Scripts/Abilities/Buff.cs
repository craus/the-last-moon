using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public int power = 0;

    public Buff ApplyNew(Creature user) {
        var buff = Instantiate(this, user.transform);
        user.buffs.Add(buff);
        return buff;
    }

    public void Apply(Creature user) {
        var buff = user.buffs.FirstOrDefault(b => b.GetType() == GetType());
        if (buff == null) {
            ApplyNew(user);
        } else {
            buff.power += power;
        }
    }

    public void UntilEndOfBattle() {
    }

    public virtual string Text() {
        return "Buff";
    }
}
