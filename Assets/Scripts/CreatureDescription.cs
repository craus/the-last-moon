using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureDescription : MonoBehaviour
{
    public Creature creature;
    public TMPro.TextMeshProUGUI text;

    public void Update() {
        text.text = $"<b>{creature.name}</b>\n\n{creature.Description()}";
    }
}
