using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool Available => effects.All(e => e.AllowUsage) && (!BattleOnly || Battle.instance != null);
    public bool BattleOnly => effects.All(e => e.BattleOnly);

    public List<AbilityEffect> effects;

    public void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }

    public string Text() {
        return string.Join("", effects.Select(e => e.Text()));
    }
}
