using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class AbilityDescription : MonoBehaviour
{
    public Ability ability;
    public TMPro.TextMeshProUGUI text;

    public void Update() {
        text.text = ability.Description(Player.instance);
    }
}
