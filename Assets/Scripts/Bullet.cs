using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletDataSO m_bulletData = null;
    Vector3 m_direction = Vector3.zero;

    Rigidbody2D m_rigidbody = null;

    int m_bulletPenetration = 1;

    public Vector2 Direction { get { return m_direction; } }

    public float CritRate { get { return m_bulletData.CritRate; } }

    public float EnemyCollision()
    {
        m_bulletPenetration--;
        if(m_bulletPenetration <= 0 )
        {
            Destroy(gameObject);
        }

        return m_bulletData.Damage;
    }

    public void InstantiateBullet(Vector3 direction, BulletDataSO bulletData)
    {
        m_direction = direction;
        m_bulletData = bulletData;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_bulletPenetration = bulletData.BulletPenetration;
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = (m_direction * m_bulletData.BulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            EnemyCollision();
        }    
    }
}
