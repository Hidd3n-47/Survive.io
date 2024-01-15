using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnEnemiesSurvival : MonoBehaviour
{
    [SerializeField]
    Transform m_player;

    [SerializeField]
    float m_minDistance = 3.0f;
    float m_minDistanceSqr;

    [SerializeField]
    float m_startDurationBetweenSpawns = 3.0f;
    [SerializeField]
    float m_minDurationBetweenSpawns = 0.4f;

    [SerializeField]
    List<Vector2> m_spawnPositions = new();

    [SerializeField]
    bool m_spawnPositionsAreChild = false;

    float m_startTime;

    bool m_pauseSpawning = false;

    public bool PauseSpawning { get { return m_pauseSpawning; } set { m_pauseSpawning = value; StartCoroutine(Spawner()); } }

    public void SetSpawns(HashSet<Vector2> spawns)
    {
        m_spawnPositions = spawns.ToList();
        StartCoroutine(Spawner());
    }

    private void Awake()
    {
        m_player = GameObject.Find("Player").transform;

        m_minDistanceSqr = m_minDistance * m_minDistance;

        if(m_spawnPositionsAreChild )
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                m_spawnPositions.Add((Vector2)(transform.position + transform.GetChild(i).position));
            }

            return;
        }
        
        Assert.IsFalse(m_spawnPositions.Count != 0, "No enemy spawn points added.");
    }

    private void Start()
    {
        m_startTime = Time.time;

        if(GameManagerSurvival.Instance)
        {
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        float timer = 0.0f;
        while(!m_pauseSpawning)
        {
            float time = Time.time - m_startTime;
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                // Reduce the timer by 0.1s for every 10 seconds that has passed.
                timer = Mathf.Clamp(m_startDurationBetweenSpawns - 0.1f * Mathf.Floor(0.1f * time), m_minDurationBetweenSpawns, m_startDurationBetweenSpawns);
                
                Vector2 spawnPosition = Vector2.zero;
                float distSquare;
                int earlyExit = 0;
                do
                {
                    spawnPosition = m_spawnPositions[Random.Range(0, m_spawnPositions.Count)];
                    distSquare = ((Vector2)m_player.transform.position - spawnPosition).sqrMagnitude;

                    if (earlyExit++ == 10)
                    {
                        Assert.IsFalse(true, "Failed to randomly find an enmy spawn position out of player range.");

                        Vector2 randPosition = Random.insideUnitCircle;
                        randPosition = randPosition.normalized;
                        spawnPosition = spawnPosition + randPosition * Random.Range(m_minDistance, m_minDistance * 2);
                    }
                } while (distSquare < m_minDistanceSqr && distSquare > 20.0f);

                EnemyManager.Instance.SpawnEnemy(spawnPosition);
            }

            yield return null;
        }
    }
}
