using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{ 
    private float m_maxHealth = 100.0f;
    private float m_health;
    private float m_iFrameTime = 0.5f;
    private bool m_iFrames = false;

    [SerializeField]
    DeathScreen m_deathScreen;

    CapsuleCollider2D m_playerCollider;
    TrailRenderer m_trailRenderer;
    LayerMask m_enemyLayerMask;

    public UnityEvent<float> PlayerDamaged;

    public float Health { get { return m_health; } private set { } }

    private void Awake()
    {
        m_health = m_maxHealth;

        m_playerCollider = GetComponent<CapsuleCollider2D>();
        m_trailRenderer = GetComponent<TrailRenderer>();
        m_enemyLayerMask = LayerMask.GetMask("EnemyCollider");
    }
    public void ActivateIFrames()
    {
        m_iFrames = true;
        m_playerCollider.excludeLayers += m_enemyLayerMask;
        StartCoroutine(ActiveIFrameTimer());
    }

    public void DeactivateIFrames()
    {
        m_iFrames = false;
        m_playerCollider.excludeLayers -= m_enemyLayerMask;
    }

    public void Heal(float amount)
    {
        m_health += amount;
        m_health = Mathf.Min(m_health, m_maxHealth);
        PlayerDamaged?.Invoke(m_health / m_maxHealth);
    }

    public void Damage(float damage)
    {
        if(m_iFrames)
        {
            return;
        }

        float damageScale = GameManagerSurvival.Instance ? GameManagerSurvival.Instance.Defense : 1.0f;
        m_health -= damage * damageScale;
        PlayerDamaged?.Invoke(m_health / m_maxHealth);
        
        if(m_health <= 0)
        {
            // Only increment deaths if in survival.
            if(GameManagerSurvival.Instance)
                PlayerStats.Instance.IncrementDeaths();

            m_deathScreen.ActivateDeathScreen();
        }
    }

    private IEnumerator ActiveIFrameTimer()
    {
        m_trailRenderer.emitting = true;
        yield return new WaitForSeconds(m_iFrameTime);
        DeactivateIFrames();
        m_trailRenderer.emitting = false;
    }
}
