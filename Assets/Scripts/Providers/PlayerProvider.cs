using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : CreatureProvider
{
    public override Creature Value => Game.instance.player;
}
