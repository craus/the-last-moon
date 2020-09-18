using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFileState
{
    public List<PlayerProfile> playerProfiles;

    public PlayerProfile currentProfile;

    public SaveFileState() {
        playerProfiles = new List<PlayerProfile>();
        currentProfile = new PlayerProfile();
        playerProfiles.Add(currentProfile);
    }
}
