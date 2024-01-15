using System.Collections;
using UnityEngine;

public class EnemySpawnByDistance : MonoBehaviour
{
    [SerializeField]
    Transform m_origin;
    [SerializeField]
    Transform m_enemy;

    [SerializeField]
    BloodParticles m_bloodParticles;
    [SerializeField]
    DamageNumberGenerator m_damageNumberGenerator;

    [SerializeField]
    float m_minDistance = 3.0f;
    [SerializeField]
    float m_maxDistance = 10.0f;

    [SerializeField]
    float m_timeBetweenSpawns = 3.0f;
    [SerializeField]
    float m_minDurationBetweenSpawns = 0.1f;

    float m_timer;

    private void Start()
    {
        m_timer = m_timeBetweenSpawns;

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            m_timer = -(1.0f / 30000.0f) * Time.time * Time.time + m_timeBetweenSpawns + m_minDurationBetweenSpawns;

            m_timer = Mathf.Clamp(m_timer, m_minDurationBetweenSpawns, m_timeBetweenSpawns);

            float randDist = Random.Range(m_minDistance, m_maxDistance);

            Vector2 direction = Random.insideUnitCircle.normalized;

            Transform t = Instantiate(m_enemy, m_origin.position + (Vector3)(direction * randDist), Quaternion.identity);

            IEnemy ienemy = t.GetComponent<IEnemy>();
            ienemy.TargetPosition = m_origin;
            GameManagerSurvival.Instance.SubscribeToOnDeathEvent(ienemy);
            Score.Instance.SubscribeToOnDeathEvent(ienemy);
            BloodParticles.Instance.SubscribeToEnemyDeathEvent(ienemy);

            EnemyDamage enemy = t.GetChild(0).GetComponent<EnemyDamage>();
            m_bloodParticles.SubscribeToEnemyEvent(enemy);
            m_damageNumberGenerator.SubscribeToEnemyEvent(enemy);
            

            yield return new WaitForSeconds(m_timer);
        }
    }
}
