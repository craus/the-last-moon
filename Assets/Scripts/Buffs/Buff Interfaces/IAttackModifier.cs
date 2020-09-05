using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// see attack modifiers

/// <summary>
/// Modifies creature attack
/// </summary>
public interface IAttackModifier : IModifier
{
    /// <summary>
    /// Call when creature is to attack.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    void ModifyAttack(Attack attack);
}
