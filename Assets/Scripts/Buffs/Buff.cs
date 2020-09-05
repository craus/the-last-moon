using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public Creature owner;

    public int power = 1;

    public Buff ApplyNew(Creature target) {
        var buff = Instantiate(this, target.buffsFolder);
        owner = target;
        target.buffs.Add(buff);
        return buff;
    }

    public void Apply(Creature target) {
        var buff = target.buffs.FirstOrDefault(b => b.GetType() == GetType());
        if (buff == null) {
            ApplyNew(target);
        } else {
            buff.power += power;
        }
    }

    public void Spend(int delta = 1) {
        power = Mathf.Clamp(power - delta, 0, int.MaxValue);
        ExpireCheck();
    }

    public void ExpireCheck() {
        if (power == 0) {
            Expire();
        }
    }

    public void Expire() {
        Destroy(gameObject);
    }

    public void OnDestroy() {
        owner.buffs.Remove(this);
    }

    public void UntilEndOfBattle() {
    }

    public virtual string Text() {
        return "Buff";
    }
}
