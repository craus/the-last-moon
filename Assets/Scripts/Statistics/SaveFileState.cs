using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFileState
{
    public List<PlayerProfile> playerProfiles = new List<PlayerProfile>() { new PlayerProfile() };
}
