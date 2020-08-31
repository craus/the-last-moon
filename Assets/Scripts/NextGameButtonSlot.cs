using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

public class NextGameButtonSlot : MonoBehaviour
{
    public Button button;

    public void Update() {
        button.gameObject.SetActive(!Player.instance.Alive);
    }
}
