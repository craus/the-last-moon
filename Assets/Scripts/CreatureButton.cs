using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreatureButton : MonoBehaviour
{
    public Creature creature;

    public void Click() {
        GameManager.instance.Click(creature);
    }
}
