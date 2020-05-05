using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListEffect : AbilityEffect
{
    public override bool RequireTarget => effects.Any(e => e.RequireTarget);

    public List<AbilityEffect> effects;

    public override void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }

    public override string Text(Creature user) {
        return effects.Select(e => e.Text(user)).Join();
    }
}
