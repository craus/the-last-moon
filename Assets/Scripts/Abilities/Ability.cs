using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool Available => effects.All(e => e.AllowUsage);

    public List<AbilityEffect> effects;

    public void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }
}
