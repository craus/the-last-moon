using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GainAbility : NonTargetEffect
{
    public Transform abilityFolder;
    public Ability ability;

    public override void Use(Creature user) {
        var player = user as Player;
        if (player == null) {
            return;
        }
        player.GainAbility(ability);
    }

    public override string Text(Creature user) {
        return "^ " + ability?.Text(user);
    }

    public override string Description(Creature user) {
        return $"Buy: {ability.Description(user)}";
    }

    public void Update() {
        if (Extensions.InEditMode()) {
            name = $"Buy {ability.name}";
        }
    }
}
