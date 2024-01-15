using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class IEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemySO m_enemyData;

    [SerializeField]
    Transform m_targetPosition;
   
    SpriteRenderer m_spriteRenderer;
    AudioSource m_audioPlayer;
    AIPath m_ai;

    public UnityEvent OnDeath;
    public UnityEvent<Vector2> OnDeathWithPosition;

    // Stats.
    float m_health;
    float m_damage;
    float m_speed;
    bool m_dying;

    public float Damage
    {
        get
        {
            return m_damage;
        }

        private set { }
    }

    public Transform TargetPosition { get { return m_targetPosition; } set { m_targetPosition = value; } }

    public void Init(Transform targetPosition)
    {
        m_targetPosition = targetPosition;

        if(GameManagerSurvival.Instance)
        {
            // Increase the enemies health by 1 for every 5 enemies killed.
            m_health += Mathf.Floor(0.2f * Score.Instance.GetScore());
            // Increase the enemies speed by 0.1 for every 5 enemies killed.
            m_speed += 0.1f * Mathf.Floor(0.2f * Score.Instance.GetScore());
            m_speed = Mathf.Clamp(m_speed, 0.0f, EnemyManager.Instance.MaxEnemySpeed);
        }

        m_ai.maxSpeed = m_speed;
        
    }
    public void Destroy() 
    { 
        EnemyManager.Instance.RemoveEnemyFromList(gameObject);
        Destroy(gameObject); 
    }

    public bool DamageEnemy(float damage)
    {
        m_health -= damage;

        if (m_health <= 0.0f)
        {
            if(!m_dying)
            {
                m_dying = true;

                m_audioPlayer.clip = m_enemyData.DeathSound;
                m_audioPlayer.Play();

                OnDeath?.Invoke();
                OnDeathWithPosition?.Invoke(transform.position);
            }

            return false;
        }

        m_audioPlayer.clip = m_enemyData.DamageSound;
        m_audioPlayer.Play();
        return true;
    }

    private void Awake()
    {
        m_health = m_enemyData.Health;
        m_damage = m_enemyData.Damage;
        m_speed = m_enemyData.Speed;

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_audioPlayer = GetComponent<AudioSource>();
        m_ai = GetComponent<AIPath>();
    }

    private void Update()
    {
        if(m_dying) { return; }
    }

    private void FixedUpdate()
    {
        if(GameManagerSurvival.Instance)
        {
            Vector3 before = transform.position;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)m_targetPosition.position, Time.deltaTime * m_speed);
            Vector3 after = transform.position;

            Vector3 difference = after - before;

            if (difference.x < 0.0f)
            {
                m_spriteRenderer.flipX = true;
            }
            else
            {
                m_spriteRenderer.flipX = false;
            }

            return;
        }

        m_ai.destination = m_targetPosition? m_targetPosition.position : Vector3.zero;

        if (m_ai.desiredVelocity.x <= 0.0f)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
    }
}
