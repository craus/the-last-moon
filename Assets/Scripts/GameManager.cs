using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Battle battleSample;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }

    public void Restart() {
        Destroy(FindObjectOfType<Battle>().gameObject);
        Instantiate(battleSample);
    }
}
