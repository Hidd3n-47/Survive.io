using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackBossDamage : MonoBehaviour
{
    [SerializeField]
    Slider m_whiteSlider;
    [SerializeField]
    float m_rate;

    float m_percentage = 1.0f;
    float m_desiredPercent = 1.0f;

    public void SetPercent(float percentage)
    {
        m_desiredPercent = percentage;
    }

    private void Awake()
    {
        StartCoroutine(SliderAdjuster());
    }

    private IEnumerator SliderAdjuster()
    {
        while(true)
        {
            if(m_percentage > m_desiredPercent)
            {
                m_percentage -= m_rate * Time.deltaTime;

                m_percentage = Mathf.Max(m_desiredPercent, m_percentage);

                m_whiteSlider.value = m_percentage;
            }
            yield return null;
        }
    }
}
