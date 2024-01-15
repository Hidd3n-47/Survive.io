using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackPlayerDamage : MonoBehaviour
{
    [SerializeField]
    private float m_lerpSpeed = 0.1f;

    private Slider m_slider;

    private float m_percentLerpingTo;

    private void Awake()
    {
        m_slider = GetComponent<Slider>();
        m_percentLerpingTo = 1.0f;
    }

    public void PlayerDamaged(float percent)
    {
        m_percentLerpingTo = percent;

        StopAllCoroutines();
        StartCoroutine(LerpDamage());
        
    }

    private IEnumerator LerpDamage()
    {
        float sliderStart = m_slider.value;

        while(m_slider.value != m_percentLerpingTo)
        {
            m_slider.value -= m_slider.value * m_lerpSpeed * Time.deltaTime;
            m_slider.value = Mathf.Clamp(m_slider.value, m_percentLerpingTo, sliderStart);
            yield return null;
        }
    }
}
