using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Modifies creature turn end
/// </summary>
public interface IEndCombatModifier : IModifier
{
    void OnCombatEnd();
}
