using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSkillPoints : BoolValueProvider
{
    public override bool Value => Game.instance.player.skillPoints > 0;
}
