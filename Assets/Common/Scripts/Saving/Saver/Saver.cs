using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;

public class Saver : MonoBehaviour
{
    public string key;

    public void Save() {
        GetComponentsInChildren<ISaver>().ForEach(saver => saver.OnSave());
    }
}
