using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAlive : BoolValueProvider
{
    protected override bool BoolValue => Player.instance.Alive;
}
