using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Boss m_boss;

    private void Awake()
    {
        m_boss = transform.parent.GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            float damage = bullet.EnemyCollision();

            float rand = Random.value;
            bool crit = rand < bullet.CritRate;
            if (crit)
            {
                damage *= 1.2f;
            }

            m_boss.Damage(damage, bullet.Direction, crit);
        }
    }
}
