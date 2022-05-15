using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSource
{
    string Text(Creature user);
    string Description(Creature user);
}
