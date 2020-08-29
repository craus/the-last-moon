using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public virtual bool Available(Creature user) => true;
    public virtual bool BattleOnly => true;
    public virtual bool RequireTarget => true;

    public new string name;

    public virtual void Use(Creature user, Creature target) {
    }

    public virtual string Text(Creature user) {
        return "?";
    }

    public virtual string Description(Creature user) {
        return "Непонятно, что делает эта способность";
    }
}
