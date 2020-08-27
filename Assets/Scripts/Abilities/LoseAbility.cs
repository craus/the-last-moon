using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseAbility : NonTargetEffect
{
    public Ability ability;

    public override void Use(Creature user) {
        var player = user as Player;
        if (player == null) {
            return;
        }
        player.LoseAbility(ability);
    }

    public override string Text(Creature user) {
        return "";// "v " + ability.Text(user);
    }
}
