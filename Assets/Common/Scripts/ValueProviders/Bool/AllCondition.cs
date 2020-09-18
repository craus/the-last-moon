using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllCondition : AggregateCondition {
    public override bool Value => Arguments.All(b => b);
}
