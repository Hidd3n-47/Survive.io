using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    static public BloodParticles Instance;
    [SerializeField]
    Vector2 m_size = Vector2.one;
    [SerializeField]
    Vector2 m_sizeVariation = Vector2.zero;
    [SerializeField, Range(0.0f, 1.0f)]
    float m_movementDuration = 0.15f;
    [SerializeField]
    float m_speed = 8.0f;
    [SerializeField]
    int m_numParticlesPerHit = 20;
    [SerializeField, Range(0.0f, 90.0f)]
    float m_spread = 5.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    float m_moveDurationVariation = 0.03f;
    [SerializeField]
    float m_speedVariation = 1.0f;
    [SerializeField]
    float m_deathMovementDuration = 0.13f;
    [SerializeField]
    float m_deathSpeed = 10.0f;
    [SerializeField]
    float m_deathSpeedVariation = 3.0f;
    [SerializeField]
    int m_deathNumParticlesPerHit = 40;

    private class BloodParticle
    {
        public BloodParticle(int index, float movementDuration, float speed, Vector2 direction)
        {
            this.index = index;
            this.movementDuration = movementDuration;
            this.speed = speed;
            this.direction = direction;
        }

        public int index;
        public float movementDuration;
        public float speed;
        public Vector2 direction;

        public Vector2 Movement { get { return direction * speed;  } }
    }

    private ParticleSystemCustom m_particles;
    List<BloodParticle> m_bloodParticles = new();

    private void Awake()
    {
        Instance = this;

        m_particles = GetComponent<ParticleSystemCustom>();
    }

    private void Update()
    {
       for(int i = 0; i < m_bloodParticles.Count; i++)
        {
            BloodParticle particle = m_bloodParticles[i];
            m_particles.AddToQuadPosition(particle.index, particle.Movement * Time.deltaTime);
            particle.movementDuration -= Time.deltaTime;

            if(particle.movementDuration <= 0.0f)
            {
                m_bloodParticles.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnBlood(Vector2 position, Vector2 direction)
    {
        for(int i = 0; i < m_numParticlesPerHit; i++)
        {
            Vector2 size = m_size + new Vector2(Random.Range(-m_sizeVariation.x, m_sizeVariation.x),
                Random.Range(-m_sizeVariation.y, m_sizeVariation.y));

            int index = m_particles.AddQuad(position, size, Random.Range(0.0f, 360.0f), Random.Range(0, 8) , 8);

            float movementDuration = m_movementDuration + Random.Range(-m_moveDurationVariation, m_moveDurationVariation);

            float speed = m_speed + Random.Range(-m_speedVariation, m_speedVariation);

            Quaternion angle = Quaternion.Euler(0.0f, 0.0f, Random.Range(-m_spread, m_spread));

            Vector2 dir = angle * (Vector3)direction;

            m_bloodParticles.Add(new BloodParticle(index, movementDuration, speed, dir));
        }
    }

    public void SpawnBloodOnDeath(Vector2 position)
    {
        for (int i = 0; i < m_deathNumParticlesPerHit; i++)
        {
            Vector2 size = m_size + new Vector2(Random.Range(-m_sizeVariation.x, m_sizeVariation.x),
                Random.Range(-m_sizeVariation.y, m_sizeVariation.y));

            Vector2 direction = Random.insideUnitCircle.normalized;

            int index = m_particles.AddQuad(position, size, Random.Range(0.0f, 360.0f), Random.Range(0, 8), 8);

            float movementDuration = m_movementDuration + Random.Range(-m_deathMovementDuration, m_deathMovementDuration);

            float speed = m_deathSpeed + Random.Range(-m_deathSpeedVariation, m_deathSpeedVariation);

            m_bloodParticles.Add(new BloodParticle(index, movementDuration, speed, direction));
        }
    }

    public void SubscribeToEnemyEvent(EnemyDamage enemy)
    {
        enemy.OnEnemyDamage.AddListener(SpawnBlood);
    }

    public void SubscribeToBossEvent(Boss boss)
    {
        boss.OnDamage.AddListener(SpawnBlood);
    }

    public void SubscribeToEnemyDeathEvent(IEnemy enemy)
    {
        enemy.OnDeathWithPosition.AddListener(SpawnBloodOnDeath);
    }
}
