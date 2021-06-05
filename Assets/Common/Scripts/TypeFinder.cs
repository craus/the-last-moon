using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class TypeFinder : MonoBehaviour
{

    [SerializeField] private Tag findThisType;

    public GameObject Find(Transform root) {
        return root.GetComponentInChildren(findThisType.GetType()).gameObject;
    }




    // to test

    [SerializeField] private Transform root;

    public void Start() {
        Debug.LogFormat($"Found object: {Find(root)}");
    }
}