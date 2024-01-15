using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance { get; private set; }

    [SerializeField]
    private BloodParticles m_particles;
    [SerializeField]
    private DamageNumberGenerator m_damageNumberGenerator;
    [SerializeField]
    private List<Transform> m_boss;
    [SerializeField]
    private List<Transform> m_bullets;
    [SerializeField]
    private Transform m_chest;

    Boss m_activeBoss;

    [SerializeField]
    GameObject m_bossHealthBar;
    [SerializeField]
    private Slider m_healthBarSlider;

    public void SetBossSpawnLocation(Vector2 position)
    {
        m_activeBoss = Instantiate(m_boss[Random.Range(0, m_boss.Count)], (Vector3)position, Quaternion.identity).GetComponent<Boss>();
        m_activeBoss.Bullet = m_bullets[Random.Range(0, m_bullets.Count)];
        m_particles.SubscribeToBossEvent(m_activeBoss);
        m_damageNumberGenerator.SubscribeToBossHitEvent(m_activeBoss);
    }

    public void HealthPercentage(float percentage)
    {
        m_healthBarSlider.value = percentage;
    }

    public void ActivateHealthBar()
    {
        m_bossHealthBar.SetActive(true);
    }

    public void Win(Vector2 position)
    {
        EnemyManager.Instance.DestroyAllEnemies();

        Instantiate(m_chest, position, Quaternion.identity);
    }

    private void Awake()
    {
        Instance = this;
    }
}
