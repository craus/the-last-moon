using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnyCondition : AggregateCondition {
    public override bool Value => Arguments.Any(a => a);
}
