using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameSelectMode : MonoBehaviour
{
    public void OnContinuePressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamemodeSelect");
    }
}
