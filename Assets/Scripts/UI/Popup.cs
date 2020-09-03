using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject hiddenDevPart;

    public void Show() {
        hiddenDevPart.SetActive(false);
        gameObject.SetActive(true);
    }
}
