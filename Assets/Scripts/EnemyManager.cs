using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField]
    Transform m_enemyOne;
    [SerializeField]
    Transform m_enemyTwo;
    [SerializeField]
    float m_maxEnemySpeed = 2.5f;

    [SerializeField]
    int m_maxEnemies = 50;

    [SerializeField]
    Transform m_player;

    [SerializeField]
    BloodParticles m_bloodParticles;
    [SerializeField]
    DamageNumberGenerator m_damageNumberGenerator;

    private List<GameObject> m_enemies = new();

    public float MaxEnemySpeed { get { return m_maxEnemySpeed; } private set { } }

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyAllEnemies()
    {
        foreach(GameObject en in m_enemies)
        {
            Destroy(en);
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        m_enemies.Remove(enemy);
    }

    public void SpawnEnemy(Vector2 spawnPosition)
    {
        if (m_enemies.Count >= m_maxEnemies)
        {
            return;
        }
        float rand = Random.value;
        Transform e = rand > 0.3f ? m_enemyOne : m_enemyTwo;

        Transform t = Instantiate(e, spawnPosition, Quaternion.identity);

        m_enemies.Add(t.gameObject);

        IEnemy ienemy = t.GetComponent<IEnemy>();
        ienemy.Init(m_player);

        if (GameManagerSurvival.Instance)
        {
            GameManagerSurvival.Instance.SubscribeToOnDeathEvent(ienemy);
            Score.Instance.SubscribeToOnDeathEvent(ienemy);
        }

        BloodParticles.Instance.SubscribeToEnemyDeathEvent(ienemy);

        EnemyDamage enemy = t.GetChild(0).GetComponent<EnemyDamage>();
        m_bloodParticles.SubscribeToEnemyEvent(enemy);
        m_damageNumberGenerator.SubscribeToEnemyEvent(enemy);
    }

}
