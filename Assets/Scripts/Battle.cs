﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : Singletone<Battle>
{
    public int moveNumber;

    public void Finish() {
        Destroy(gameObject);
        AbilitiesController.instance.currentAbility = null;
    }
}
