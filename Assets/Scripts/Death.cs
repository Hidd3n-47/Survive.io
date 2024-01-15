using UnityEngine;
using TMPro;

public class Death : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_text;

    void Awake()
    {
        m_text?.SetText("Score: " + Score.Instance.GetScore().ToString());
    }
}
