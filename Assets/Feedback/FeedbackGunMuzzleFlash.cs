using System.Collections;
using UnityEngine;

public class FeedbackGunMuzzleFlash : IFeedback
{
    SpriteRenderer m_spriteRenderer = null;

    [SerializeField]
    private float m_duration = 0.1f;

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        m_spriteRenderer.enabled = false;
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MuzzleFlash());
    }

    private void Start()
    {
        m_spriteRenderer = GameObject.Find("MuzzleFlash").GetComponent<SpriteRenderer>();
    }

    IEnumerator MuzzleFlash()
    {
        m_spriteRenderer.enabled = true;
        yield return new WaitForSeconds(m_duration);
        m_spriteRenderer.enabled = false;
    }
}
