using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class NextBattleButtonSlot : MonoBehaviour
{
    public Button button;

    public void Update() {
        button.gameObject.SetActive(!Battle.On && Player.instance.Alive);
    }
}
