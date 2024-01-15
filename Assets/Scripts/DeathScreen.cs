using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    GameObject m_pauseButton;
    GameObject m_highscorePrompt;
    public void ActivateDeathScreen()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
        m_pauseButton.SetActive(false);

        if(GameManagerSurvival.Instance)
        {
            int score = Score.Instance.GetScore();
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
            if(PlayerStats.Instance.UpdateScore(score))
            {
                m_highscorePrompt.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        if(GameManagerSurvival.Instance)
            m_highscorePrompt = transform.GetChild(2).gameObject;
    }
}
