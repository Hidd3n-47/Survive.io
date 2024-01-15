using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartPlay()
    {
        SceneManager.LoadScene("GamemodeSelect");
    }

    public void StartMiddle()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
