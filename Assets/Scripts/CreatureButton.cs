using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CreatureButton : MonoBehaviour
{
    public Creature creature;
    public Button button;

    public void Click() {
        Game.instance.Click(creature);
    }

    public void Update() {
        button.interactable = creature.Targetable;
    }
}
