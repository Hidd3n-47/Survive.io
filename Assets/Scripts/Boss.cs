using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float m_attackFrequency;
    [SerializeField]
    private float m_moveCooldown;
    [SerializeField]
    private float m_health;
    [SerializeField]
    private float m_attackRange;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private Transform m_bullet;

    private Transform m_player;

    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidBody;
    private AudioSource m_audioSource;
    private SpawnEnemiesSurvival m_enemySpawner;

    private bool m_canMove = true;
    private float m_maxHealth;

    public UnityEvent<Vector2, Vector2> OnDamage;
    public UnityEvent<Vector2, int, bool> BossDamaged;
    public UnityEvent Death;

    public Transform Bullet { get { return m_bullet; } set { m_bullet = value; } }

    public bool CanMove {  get { return m_canMove; }  set { m_canMove = value; } }

    public void Damage(float damage, Vector2 bulletDirection, bool crit)
    {
        m_health -= damage;

        OnDamage?.Invoke(transform.position, bulletDirection);
        BossDamaged?.Invoke(transform.position, (int)damage, crit);

        BossManager.Instance.HealthPercentage(m_health / m_maxHealth);

        m_audioSource.Play();

        if (m_health < 0 )
        {
            BossManager.Instance.Win(transform.position);
            m_canMove = false;

            Destroy(m_rigidBody);
            m_rigidBody = null;

            Death?.Invoke();
        }
    }

    private void Awake()
    {
        m_player = GameObject.Find("Player").transform;

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_enemySpawner = GameObject.Find("EnemySpawner").GetComponent<SpawnEnemiesSurvival>();

        m_maxHealth = m_health;
    }

    private void Attack(Vector2 direction)
    {
        IBossBullet bullet = Instantiate(m_bullet, transform.position, Quaternion.identity).GetComponent<IBossBullet>();
        bullet.Init(m_player, direction);

        StartCoroutine(MoveCooldown());
    }

    private void FixedUpdate()
    {
        if(m_rigidBody == null)
        {
            return;
        }

        m_rigidBody.velocity = Vector3.zero;

        if (!m_canMove)
        {
            return;
        }

        Vector2 direction = m_player.position - transform.position;
        float distance = direction.magnitude;
        direction /= distance;

        if(distance > 10.0f)
        {
            // Player out of range.
            return;
        }

        BossManager.Instance.ActivateHealthBar();

        m_enemySpawner.PauseSpawning = true;

        if (distance > m_attackRange)
        {
            MoveToPlayer(direction);
            return;
        }

        // Within attacking range.
        float rand = UnityEngine.Random.value;
        if(rand < m_attackFrequency) 
        {
            Attack(direction);
        }
        else
        {
            MoveToPlayer(direction);
        }
    }

    private void MoveToPlayer(Vector3 direction)
    {
        m_rigidBody.velocity = direction * m_speed;

        if (m_rigidBody.velocity.x < 0.0f)
        {
            m_spriteRenderer.flipX = true;
        }
        else
        {
            m_spriteRenderer.flipX = false;
        }
    }

    private IEnumerator MoveCooldown()
    {
        m_canMove = false;
        yield return new WaitForSeconds(m_moveCooldown);
        m_canMove = true;
    }
}
