using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public Player player;

    public void Click(Creature creature) {
        if (player.Alive && BattleUI.instance.currentAbility != null) {
            player.UseAbility(BattleUI.instance.currentAbility, creature);
            if (player.stunned < 0) {
                player.stunned++;
            } else {
                FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
            }
        }
    }
}
