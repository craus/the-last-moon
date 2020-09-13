using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GainAbility : NonTargetEffect
{
    public Transform abilityFolder;
    public Ability ability;

    public bool gainCopy = false;

    public override void Use(Creature user) {
        var player = user as Player;
        if (player == null) {
            return;
        }
        if (gainCopy) {
            player.GainAbility(Instantiate(ability));
        } else {
            player.GainAbility(ability);
        }
    }

    public override string Text(Creature user) {
        return "^ " + ability?.Text(user);
    }

    public override string Description(Creature user) {
        if (ability == null) {
            return $"Buy";
        }
        return $"{ability.Description(user)}";
    }

    public void Start() {
        if (!Extensions.InEditMode()) {
            GetComponent<Ability>().name = ability.name;
            abilityFolder.gameObject.SetActive(false);
        }
    }

    public void Update() {
        if (Extensions.InEditMode()) {
            if (ability == null) {
                return;
            }
            name = $"Buy {ability.name}";
            GetComponent<Ability>().name = ability.name;
        }
    }
}
