using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    GameObject m_pauseCanvas;

    public void OnPauseButtonPress()
    {
        m_pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void OnResumePressed()
    {
        m_pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnGamemodePressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamemodeSelect");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    private void Awake()
    {
        m_pauseCanvas = transform.GetChild(1).gameObject;
        m_pauseCanvas.SetActive(false);
    }
}
