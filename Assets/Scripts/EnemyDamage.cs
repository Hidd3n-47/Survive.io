using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamage : MonoBehaviour
{
    private IEnemy m_parentEnemy = null;
    private bool m_damageCooldownActive = false;

    [SerializeField]
    private float m_damageCooldown = 0.075f;
    [SerializeField]
    private float m_knockbackAmount = 5.0f;
    [SerializeField]
    SpriteRenderer m_spriteRenderer = null;

    Rigidbody2D m_rigidbody;

    public UnityEvent<Vector2, Vector2> OnEnemyDamage;
    public UnityEvent<Vector2, int, bool> EnemyHit;

    public float GetDamage()
    {
        if(m_damageCooldownActive)
        {
            return 0.0f;
        }

        StartCoroutine(DamageCooldown());
        return m_parentEnemy.Damage;
    }

    private void Awake()
    {
        m_parentEnemy = transform.parent.GetComponent<IEnemy>();
        m_rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            float damageMultiplier = GameManagerSurvival.Instance ? GameManagerSurvival.Instance.DamageMultiplier : 1.0f;
            float damage = bullet.EnemyCollision() * damageMultiplier;

            float rand = Random.value;
            bool crit = rand < bullet.CritRate;
            if(crit)
            {
                damage *= 1.2f;
            }

            bool alive = m_parentEnemy.DamageEnemy(damage);

            if(!alive)
            {
                return;
            }

            bullet.GetComponentInChildren<FeedbackBulletHitEnemy>().SubscribeToHitEvent(this);

            m_rigidbody.AddForce(bullet.Direction * m_knockbackAmount, ForceMode2D.Impulse);

            OnEnemyDamage?.Invoke((Vector2)transform.position, bullet.Direction);
            EnemyHit?.Invoke((Vector2)transform.position, (int)damage, crit);
        }

        SpriteRenderer spriteRenderer = collision.transform.parent?.GetComponent<SpriteRenderer>();

        if(spriteRenderer == null) { return; }

        if(collision.transform.position.y < transform.position.y)
        {
            m_spriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        }
        else
        {
            m_spriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 2;
        }
    }

    private IEnumerator DamageCooldown()
    {
        m_damageCooldownActive = true;
        yield return new WaitForSeconds(m_damageCooldown);
        m_damageCooldownActive = false;
    }
}
