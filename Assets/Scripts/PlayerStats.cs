using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private PlayerData m_playerData;

    public bool UpdateScore(int score)
    {
        if(m_playerData.maxScore < score)
        {
            m_playerData.maxScore = score;
            SaveSystem.SavePlayer(m_playerData);
            return true;
        }
        return false;
    }

    public string GetName()
    {
        return m_playerData.playerName;
    }

    public int GetEquipped()
    {
        return m_playerData.equipedGunUuid;
    }

    public void SetName(string name)
    {
        m_playerData.playerName = name;
    }

    public void UnlockGun(int index)
    {
        if(m_playerData.gunUuids.Count ==  0)
        {
            m_playerData.gunUuids = new();
        }

        if(m_playerData.gunUuids.Contains(index) || GunManager.Instance.GetNumberOfGuns() <= index)
        {
            return;
        }

        m_playerData.gunUuids.Add(index);
    }

    public void SetEquipped(int index)
    {
        m_playerData.equipedGunUuid = index;
    }

    public int GetNextGunIndex()
    {
        return m_playerData.gunUuids.Count;
    }

    public int GetHighScore()
    {
        return m_playerData.maxScore;
    }

    public int GetKills()
    {
        return m_playerData.kills;
    }

    public int GetDeaths()
    {
        return m_playerData.deaths;
    }

    public void IncrementKills()
    {
        m_playerData.kills++;
        SaveSystem.SavePlayer(m_playerData);
    }

    public void IncrementDeaths()
    {
        m_playerData.deaths++;
        SaveSystem.SavePlayer(m_playerData);
    }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);

        m_playerData = SaveSystem.LoadPlayer();

        if(m_playerData == null || m_playerData.playerName == "")
        {
            m_playerData = new();
            SceneManager.LoadScene("Tutorial");
        }
    }

    private void OnDestroy()
    {
        SaveSystem.SavePlayer(m_playerData);
    }
}
