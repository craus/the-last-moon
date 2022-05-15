using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ToggleActive : MonoBehaviour
{
    public GameObject target;

    public void Toggle() {
        target.SetActive(!target.activeSelf);
    }
}
