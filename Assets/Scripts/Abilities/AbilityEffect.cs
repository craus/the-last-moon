using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEffect : Common.Effect
{
    public virtual bool Available => !BattleOnly || Game.instance.battleOn;
    public virtual bool RequireTarget => true;
    public virtual bool AllowUsage(Creature user) => true;
    public virtual bool BattleOnly => true;

    public virtual void Use(Creature user, Creature target) {
    }

    public override void Run() {
    }

    public string manualDescription;
    public string manualText;

    public virtual string Text(Creature user) {
        return manualText != "" ? manualText : "?";
    }

    public virtual string Description(Creature user) {
        return manualDescription;
    }

    public override string ToString() {
        return GetComponentInParent<GenericAbility>().name;
    }
}
