using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamemodeSelectManager : MonoBehaviour
{
    [SerializeField]
    GunDisplay m_gunDisplay;

    public void OnSurvivalPress()
    {
        SceneManager.LoadScene("SurvivalScene");
    }

    public void OnDungeonPress()
    {
        SceneManager.LoadScene("DungeonScene");
    }

    public void OnBackPress()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnEquipPress()
    {

    }

    private void Start()
    {
        m_gunDisplay.DisplayGuns();

    }
}
