using System.Collections.Generic;
using UnityEngine;

public class BulletShellParticles : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    float m_movementDuration = 0.15f;
    [SerializeField]
    float m_speed = 8.0f;
    [SerializeField, Range(0.0f, 90.0f)]
    float m_spread = 5.0f;
    [SerializeField, Range(0.0f, 1.0f)]
    float m_moveDurationVariation = 0.03f;
    [SerializeField]
    float m_speedVariation = 1.0f;

    [SerializeField]
    Vector2 m_size = new Vector2(100.13f, 100.13f);

    private class ShellParticle
    {
        public ShellParticle(int index, float movementDuration, float speed, Vector2 position, Vector2 direction)
        {
            this.index = index;
            this.movementDuration = movementDuration;
            this.speed = speed;
            this.position = position;
            this.direction = direction;
        }

        public int index;
        public float movementDuration;
        public float speed;
        public Vector2 position;
        public Vector2 direction;

        public Vector2 Movement { get { position += direction * speed * Time.deltaTime; return position;  } }
    }


    private ParticleSystemCustom m_particles;
    List<ShellParticle> m_shellParticles = new();

    private void Awake()
    {
        m_particles = GetComponent<ParticleSystemCustom>();
    }

    private void Update()
    {
       for(int i = 0; i < m_shellParticles.Count; i++)
        {
            ShellParticle particle = m_shellParticles[i];

            m_particles.UpdateQuadVertices(particle.index, particle.Movement, m_size, Random.Range(0.0f, 360.0f));
            particle.movementDuration -= Time.deltaTime;

            if(particle.movementDuration <= 0.0f)
            {
                m_shellParticles.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnShell(Vector2 position)
    {
        int index = m_particles.AddQuad(position, m_size, Random.Range(0.0f, 360.0f));

        float movementDuration = m_movementDuration + Random.Range(-m_moveDurationVariation, m_moveDurationVariation);

        float speed = m_speed + Random.Range(-m_speedVariation, m_speedVariation);

        Quaternion angle = Quaternion.Euler(0.0f, 0.0f, Random.Range(-m_spread, m_spread));

        Vector2 dir = angle * Vector3.down;

        m_shellParticles.Add(new ShellParticle(index, movementDuration, speed, position, dir));
    }
}
