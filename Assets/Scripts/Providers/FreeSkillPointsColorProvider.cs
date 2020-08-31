using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSkillPointsColorProvider : ColorProvider
{
    public Color freeSkillPoints;
    public Color noFreeSkillPoints;

    public override Color Value => Game.instance.player.skillPoints > 0 ? freeSkillPoints : noFreeSkillPoints;
}
