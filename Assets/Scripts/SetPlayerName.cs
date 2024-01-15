using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_name;
    [SerializeField]
    TextMeshProUGUI m_kills;
    [SerializeField]
    TextMeshProUGUI m_deaths;

    private void Awake()
    {
        m_name.text = PlayerStats.Instance.GetName();
        m_kills.text = PlayerStats.Instance.GetKills().ToString();
        m_deaths.text = PlayerStats.Instance.GetDeaths().ToString();
    }
}
