using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI m_text;

    int m_score;
    public void SubscribeToOnDeathEvent(IEnemy enemy) { enemy.OnDeath.AddListener(IncrementScore); }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void IncrementScore()
    {
        m_score++;

        // Only increment kills if in survival.
        PlayerStats.Instance.IncrementKills();

        m_text.SetText(m_score.ToString());
    }

    public int GetScore() { return m_score; }

    public void SaveScore()
    {
        PlayerStats.Instance.UpdateScore(m_score);
    }
}
