using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSelf : NonTargetEffect
{
    public Buff buff;

    public override void Use(Creature user) {
        buff.Apply(user);
    }

    public override string Text(Creature user) {
        return buff.Text();
    }
}
