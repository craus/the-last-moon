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

#if UNITY_EDITOR
    public void AddAbilityToStore(Ability ability) {
        var buyAbility = Instantiate(buyAbilitySample);
        buyAbility.transform.SetParent(buyAbilitiesFolder);
        EditorUtility.SetDirty(buyAbility.gameObject);
        EditorUtility.SetDirty(buyAbilitiesFolder.gameObject);
        buyAbility.GetComponent<GainAbility>().ability = ability;
        ability.transform.SetParent(buyAbility.GetComponent<GainAbility>().abilityFolder);
        EditorUtility.SetDirty(ability.gameObject);
        EditorUtility.SetDirty(buyAbility.GetComponent<GainAbility>().abilityFolder.gameObject);
    }

    [ContextMenu("Add all abilities to store")]
    public void AddAllAbilitiesToStore() {
        transform.Children().ForEach(c => {
            var a = c.GetComponent<Ability>();
            if (a.GetComponent<GainAbility>() == null) {
                AddAbilityToStore(a);
            }
        });
    }
#endif
}
