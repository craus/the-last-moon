using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public List<AbilityEffect> effects;

    public void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }
}
