﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using UnityEngine.Events;
using System;

[ExecuteInEditMode]
public class GlobalEvents : Singletone<GlobalEvents>
{
    public Action<Creature> onDeath = c => { };
    public Action<Battle> onBattleStart = b => { };
    public Action<Game> onGameStart = g => { };
    public Action<Battle> onBattleEnd = b => { };
}
