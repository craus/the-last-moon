using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Library : Singletone<Library>
{
    [ContextMenu("Generate keys")]
    public void GenerateKeys() {
        var keys = new HashSet<string>();

        this.GetOuterComponentsInChildren<Saver>().ForEach(s => {
            if (s.key == "") {
                s.key = s.transform.Path(transform);
            }
            if (keys.Contains(s.key)) {
                Debug.LogErrorFormat("Duplicate key: {0}", s.key);
            }
            keys.Add(s.key);
        });
    }
}
