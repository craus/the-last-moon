using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : MonoBehaviour
{
    public virtual bool RequireTarget => true;
    public virtual bool AllowUsage => true;

    public abstract void Use(Creature user, Creature target);
}
