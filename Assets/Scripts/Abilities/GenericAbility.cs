using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericAbility : Ability
{
    public override bool Available => effects.All(e => e.AllowUsage) && (effects.Any(e => e.Available) || effects.Count() == 0);
    public override bool BattleOnly => effects.All(e => e.BattleOnly);
    public override bool RequireTarget => effects.Any(e => e.RequireTarget);

    public List<AbilityEffect> effects;

    public override void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }

    public override string Text(Creature user) {
        return string.Join("", effects.Select(e => e.Text(user)));
    }
}
