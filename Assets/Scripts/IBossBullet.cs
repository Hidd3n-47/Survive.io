using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBossBullet : MonoBehaviour
{
    [SerializeField]
    private float m_damage;
    [SerializeField]
    private float m_lifeTime;
    [SerializeField]
    private float m_explosionOnDeathRadius = 0.0f;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private Transform m_onHitEffect;

    private Transform m_player;
    private Vector2 m_direction;

    public void Init(Transform player, Vector2 direction)
    {
        m_player = player;
        m_direction = direction;
    }

    private void Update()
    {
        m_lifeTime -= Time.deltaTime;

        if(m_lifeTime <= 0.0f)
        {
            OnDeathWithNoPlayerCollision();
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3)m_direction * m_speed;

        float angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnDeathWithNoPlayerCollision()
    {
        if((m_player.position - transform.position).magnitude < m_explosionOnDeathRadius)
        {
            m_player.GetComponent<Player>().Damage(m_damage);
            BlowUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_player.GetComponent<Player>().Damage(m_damage);
            BlowUp();
        }
    }

    private void BlowUp()
    {
        Instantiate(m_onHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
