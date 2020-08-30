using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : MonoBehaviour
{
    public virtual bool Available => !BattleOnly || Battle.On;
    public virtual bool RequireTarget => true;
    public virtual bool AllowUsage(Creature user) => true;
    public virtual bool BattleOnly => true;

    public abstract void Use(Creature user, Creature target);

    public string manualDescription;
    public string manualText;

    public virtual string Text(Creature user) {
        return manualText != "" ? manualText : "?";
    }

    public virtual string Description(Creature user) {
        return manualDescription;
    }
}
