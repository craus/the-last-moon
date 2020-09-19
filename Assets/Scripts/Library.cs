using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Library : Singletone<Library>
{
    public Map<string, Saver> objects;

    public bool inited = false;

#if UNITY_EDITOR
    [ContextMenu("Generate keys")]
    public void GenerateKeys() {
        var keys = new HashSet<string>();

        this.GetOuterComponentsInChildren<Saver>().ForEach(s => {
            if (s.key == "") {
                s.key = s.transform.Path(transform);
                EditorUtility.SetDirty(s);
            }
            if (keys.Contains(s.key)) {
                Debug.LogErrorFormat("Duplicate key: {0}", s.key);
            }
            keys.Add(s.key);
        });
    }
#endif

    public void Initialize() {
        if (inited) {
            return;
        }
        inited = true;
        objects = new Map<string, Saver>();
        this.GetOuterComponentsInChildren<Saver>().ForEach(s => {
            objects[s.key] = s;
        });
    }

    public Saver GetByKey(string key) {
        Initialize();
        return objects[key];
    }
}
