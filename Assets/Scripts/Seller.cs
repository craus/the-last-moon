using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Seller : Singletone<Store>
{
    public Ability sellAbilitySample;

    public void StartSell() {
        Player.instance.sellAbilitiesFolder.gameObject.SetActive(true);
        Player.instance.sellAbilitiesFolder.Clear();
        Player.instance.abilitiesFolder.Children().ForEach(CreateSellAbility);
    }

    private void CreateSellAbility(Transform t) {
        var ability = t.GetComponent<Ability>();

        var sellAbility = Instantiate(sellAbilitySample);
        sellAbility.transform.SetParent(Player.instance.sellAbilitiesFolder);
    }

    public void FinishSell() {
        Player.instance.sellAbilitiesFolder.gameObject.SetActive(false);
    }

    public void OnDisable() {
        FinishSell();
    }
}
