using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainAbility : NonTargetEffect
{
    public Ability ability;

    public override void Use(Creature user) {
        var player = user as Player;
        if (player == null) {
            return;
        }
        player.GainAbility(ability);
    }

    public override string Text(Creature user) {
        return "^ " + ability.Text(user);
    }
}
