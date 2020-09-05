using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Modifies some game event
/// </summary>
public interface IModifier
{
    /// <summary>
    /// The greater priority is, the later modifier would be applied
    /// </summary>
    int Priority { get; }
}
