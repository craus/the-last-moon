using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleOn : BoolValueProvider
{
    public override bool Value => Game.instance.battleOn;
}
