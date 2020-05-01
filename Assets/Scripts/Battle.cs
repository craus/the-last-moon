using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public Player player;

    public void Click(Creature creature) {
        if (player.Alive) {
            player.Attack(creature);
            FindObjectsOfType<Monster>().ForEach(m => m.MakeMove());
        }
    }
}
