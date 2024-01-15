using System.Collections;
using UnityEngine;

public class DamageNumberGenerator : MonoBehaviour
{
    [SerializeField]
    Transform m_damageNumberPrefab;
    [SerializeField]
    Transform m_damageCritNumberPrefab;

    [SerializeField]
    float m_heightOffset;

    [SerializeField]
    float m_scaleTimer;
    [SerializeField]
    float m_scaleTimerVariation;

    [SerializeField]
    float m_startSize;
    [SerializeField]
    float m_startSizeVariation;
    [SerializeField]
    float m_endSize;
    [SerializeField]
    float m_endSizeVariation;

    public void CreateDamagePopup(Vector2 position, int damage, bool crit)
    {
        Vector3 spawnPosition = (Vector3)(position + new Vector2(Random.Range(m_heightOffset, 1.5f * m_heightOffset), Random.Range(0.0f, m_heightOffset)));
        
        Transform number = crit ? m_damageCritNumberPrefab : m_damageNumberPrefab;
        TextMesh textMesh = Instantiate(number, spawnPosition, Quaternion.identity).GetComponent<TextMesh>();

        textMesh.text = damage.ToString();

        float startSize = m_startSize + Random.Range(-m_startSizeVariation, m_startSizeVariation);
        float endSize = m_endSize + Random.Range(-m_endSizeVariation, m_endSizeVariation);
        float duration = m_scaleTimer + Random.Range(-m_scaleTimerVariation, m_scaleTimerVariation);

        StartCoroutine(ScaleFontUp(textMesh, startSize, endSize, duration));
        StartCoroutine(ScaleFontDown(textMesh, endSize, startSize, duration));
    }

    private void DestoyText(TextMesh text)
    {
        Destroy(text.gameObject);
    }

    IEnumerator ScaleFontUp(TextMesh text, float start, float end, float duration)
    {
        float timer = duration;
        while(timer > 0)
        {
            text.characterSize = Mathf.Lerp(start, end, timer);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ScaleFontDown(TextMesh text, float start, float end, float duration)
    {
        yield return new WaitForSeconds(m_scaleTimer);

        float timer = duration;
        while (timer > 0)
        {
            text.characterSize = Mathf.Lerp(start, end, timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        DestoyText(text);
    }

    public void SubscribeToEnemyEvent(EnemyDamage enemy)
    {
        enemy.EnemyHit.AddListener(CreateDamagePopup);
    }

    public void SubscribeToBossHitEvent(Boss boss)
    {
        boss.BossDamaged.AddListener(CreateDamagePopup);
    }
}
