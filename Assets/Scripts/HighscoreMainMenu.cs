using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreMainMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "High score: " + PlayerStats.Instance.GetHighScore().ToString();
    }
}
