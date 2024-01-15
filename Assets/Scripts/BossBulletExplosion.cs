using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletExplosion : MonoBehaviour
{
    float m_deathTimer;

    private void Awake()
    {
        // Add 0.1f for padding.
        m_deathTimer = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.1f;
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(m_deathTimer);
        Destroy(gameObject);
    }
}
