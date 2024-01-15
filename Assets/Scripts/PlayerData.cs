using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public PlayerData()
    {
        gunUuids = new();
        playerName = "";
        equipedGunUuid = 0;
        maxScore = 0;
    }

    public List<int> gunUuids;
    public string playerName;
    public int equipedGunUuid;
    public int maxScore;
    public int kills;
    public int deaths;
}
