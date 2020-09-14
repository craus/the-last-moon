using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoreOn : BoolValueProvider
{
    public override bool Value => Game.instance.storeOn;
}
