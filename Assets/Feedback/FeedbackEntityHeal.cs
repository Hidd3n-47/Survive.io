using System.Collections;
using UnityEngine;

public class FeedbackEntityHeal : IFeedback
{
    [SerializeField]
    float m_flashDuration = 0.2f;
    [SerializeField]
    Material m_material = null;

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
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        m_spriteRenderer.material.SetColor("_SolidColor", new Color(30.0f / 255.0f, 215.0f / 255.0f, 25.0f / 255.0f));
        m_spriteRenderer.material.SetInt("_MakeSolidColor", 1);
        yield return new WaitForSeconds(m_flashDuration);
        m_spriteRenderer.material.SetInt("_MakeSolidColor", 0);

    }
}
