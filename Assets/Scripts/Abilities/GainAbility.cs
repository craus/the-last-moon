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
        return $"{ability.Description(user)}";
    }

    public void Start() {
        GetComponent<Ability>().name = ability.name;
    }

    public void Update() {
        if (Extensions.InEditMode()) {
            name = $"Buy {ability.name}";
            GetComponent<Ability>().name = ability.name;
        }
    }
}
