using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public Player player;

    public void Click(Creature creature) {
        if (player.Alive && UI.instance.currentAbility != null) {
            player.UseAbility(UI.instance.currentAbility, creature);
            FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
        }
    }
}
