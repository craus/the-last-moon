﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public virtual bool Available => true;
    public virtual bool BattleOnly => true;
    public virtual bool RequireTarget => true;

    public virtual void Use(Creature user, Creature target) {
    }

    public virtual string Text(Creature user) {
        return "?";
    }
}
