using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class AbilityText : MonoBehaviour
{
    public Ability ability;
    public TMPro.TextMeshProUGUI text;
    public string format = "{damage}{splash}{stun}";

    public string EffectText(AbilityEffect e) {
        if (e is DealDamage) {
            return (e as DealDamage).damage.ToString();
        }
        if (e is DealSplashDamage) {
            return "S" + (e as DealSplashDamage).damage.ToString();
        }
        if (e is Heal) {
            return "H" + (e as Heal).heal.ToString();
        }
        if (e is Stun) {
            return "*".repeat((e as Stun).stun);
        }
        return "?";
    }

    public void Update() {
        text.text = string.Join("", ability.effects.Select(EffectText));
    }
}
