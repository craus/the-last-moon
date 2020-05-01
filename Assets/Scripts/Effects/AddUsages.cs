using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUsages : Common.Effect
{
    public Spend spend;
    public int usages;

    public override void Run() {
        spend.usages += usages;
    }
}
