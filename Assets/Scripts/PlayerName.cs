using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [SerializeField]
    GameObject m_namePanel;

    string m_value;

    public void OnStringEnter(string value)
    {
        m_value = value;
    }

    public void OnConfirm()
    {
        if(m_value == "")
        {
            return;
        }

        PlayerStats.Instance.SetName(m_value);
        m_namePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Awake()
    {
        Time.timeScale = 0.0f;
    }
}
