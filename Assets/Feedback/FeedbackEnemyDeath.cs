using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackEnemyDeath : IFeedback
{
    [SerializeField]
    IEnemy m_enemy;
    [SerializeField]
    float m_deathDuration = 0.2f;

    SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
    }

    public override void CreateFeedback()
    {
        StartCoroutine(DeathFeedback());
    }

    IEnumerator DeathFeedback()
    {
        float timer = 1.0f;

        while (timer > 0.0f)
        {
            float amount = Mathf.Lerp(0.0f, 1.0f, timer);
            timer -= Time.deltaTime * (1.0f / m_deathDuration);

            m_spriteRenderer.material.SetFloat("_DissolveScale", amount);
            yield return null;
        }

        m_enemy.Destroy();
    }
}
