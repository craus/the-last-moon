using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

public class Store : Singletone<Store>
{
    public Ability buyAbilitySample;
    public Transform buyAbilitiesFolder;
    public Transform goodsFolder;

    public void Start() {
        goodsFolder.gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    public void AddAbilityToStore(Ability ability) {
        var buyAbility = PrefabUtility.InstantiatePrefab(buyAbilitySample) as Ability;
        buyAbility.transform.SetParent(buyAbilitiesFolder);
        EditorUtility.SetDirty(buyAbility.gameObject);
        EditorUtility.SetDirty(buyAbilitiesFolder.gameObject);
        buyAbility.GetComponent<GainAbility>().ability = ability;
        ability.transform.SetParent(goodsFolder);
        EditorUtility.SetDirty(ability.gameObject);
        EditorUtility.SetDirty(goodsFolder.gameObject);
    }

    [ContextMenu("Add all abilities to store")]
    public void AddAllAbilitiesToStore() {
        buyAbilitiesFolder.Children().ForEach(c => {
            var a = c.GetComponent<Ability>();
            if (a?.GetComponent<GainAbility>() == null) {
                AddAbilityToStore(a);
            }
        });
    }

    [ContextMenu("Move all abilities to folder")]
    public void MoveAllAbilitiesToFolder() {
        buyAbilitiesFolder.Children().ForEach(c => {
            var a = c.GetComponent<Ability>();
            var ba = a?.GetComponent<GainAbility>();
            if (ba != null) {
                ba.ability.transform.SetParent(goodsFolder);
                EditorUtility.SetDirty(ba.ability.gameObject);
                EditorUtility.SetDirty(goodsFolder.gameObject);
            }
        });
    }
#endif
}
