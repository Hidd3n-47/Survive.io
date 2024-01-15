using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private float m_healthPerHeal = 10.0f;

    [SerializeField]
    private float m_destroyTimer = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        GameManagerSurvival.Instance.HealPlayer(m_healthPerHeal);
       
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        StartCoroutine(DestroyPickup());
    }

    private IEnumerator DestroyPickup()
    {
        float timer = 1.0f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 

        while (timer > 0.0f)
        {
            float amount = Mathf.Lerp(0.0f, 1.0f, timer);
            timer -= Time.deltaTime / m_destroyTimer;

            spriteRenderer.material.SetFloat("_DissolveScale", amount);
            yield return null;
        }

        Destroy(gameObject);
    }
}
