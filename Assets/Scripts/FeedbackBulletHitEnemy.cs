using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBulletHitEnemy : MonoBehaviour
{
    [SerializeField]
    Transform m_bulletHitEnemyFeedback;

    public void PlayBulletHitEnemyFeedback(Vector2 enemyPosition, int damage, bool crit)
    {
        Vector2 offset = Random.insideUnitCircle * 0.4f;
        Instantiate(m_bulletHitEnemyFeedback, (Vector3)(enemyPosition + offset), Quaternion.identity);
    }

    public void SubscribeToHitEvent(EnemyDamage enemy)
    {
        enemy.EnemyHit.AddListener(PlayBulletHitEnemyFeedback);
    }
}
