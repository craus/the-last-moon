using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public Player player;
    public int moveNumber;

    public void Click(Creature creature) {
        if (BattleUI.instance.currentAbility != null) {
            UseAbility(BattleUI.instance.currentAbility, creature);
        }
    }

    public void UseAbility(Ability a, Creature target = null) {
        if (player.Alive) {
            player.UseAbility(a, target);
            moveNumber++;
            player.AfterMove();
            if (player.stunned < 0) {
                player.stunned++;
            } else {
                FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
            }
        }
    }
}
