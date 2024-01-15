using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    Player m_player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy"))
        {
            return;
        }

        float damage = collision.GetComponent<EnemyDamage>().GetDamage();
        if (damage > 0.0f)
        {
            m_player.Damage(damage);
        }
    }
}
