using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class GenericAbility : Ability
{
    public override bool Available(Creature user) => effects.All(e => e.AllowUsage(user)) && (effects.Any(e => e.Available) || effects.Count() == 0);
    public override bool BattleOnly => effects.All(e => e.BattleOnly);
    public override bool RequireTarget => effects.Any(e => e.RequireTarget);

    public List<AbilityEffect> effects;

    [TextArea]
    public string manualDescription = "";

    [TextArea]
    [SerializeField]
    [ReadOnly]
    private string descriptionPreview = "";

    public string manualText = "";

    public override void Use(Creature user, Creature target) {
        effects.ForEach(e => e.Use(user, target));
    }

    public override string Text(Creature user) {
        if (manualText != "") {
            return manualText;
        }
        if (effects.Count == 0) {
            return "pass";
        }
        return string.Join("", effects.Select(e => e.Text(user)));
    }

    public override string Description(Creature user) {
        if (manualDescription != "") {
            return manualDescription;
        }
        if (effects.Count > 0) {
            return string.Join("\n", effects.Select(e => e.Description(user)));
        } else {
            return "End turn";
        }
    }

    public void Update() {
        if (Extensions.InEditMode()) {
            if (GetComponent<GainAbility>() == null) {
                gameObject.name = this.name;
            }
            descriptionPreview = Description(null);
        }
    }
}
