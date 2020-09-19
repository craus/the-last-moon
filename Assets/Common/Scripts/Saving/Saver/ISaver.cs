using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;

public interface ISaver
{
    void OnSave();

    void OnLoad();
}
