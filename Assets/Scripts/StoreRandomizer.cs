using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

public class StoreRandomizer : MonoBehaviour
{
    public Transform store;
    public Transform goodsFolder;

    public int itemsInStore;

    public void Start() {
        goodsFolder.gameObject.SetActive(false);
        RandomizeStore();
    }

    public void RandomizeStore() {
        goodsFolder.Children().RndSelection(itemsInStore).Shuffled().ForEach(item => item.transform.SetParent(store));
    }
}
