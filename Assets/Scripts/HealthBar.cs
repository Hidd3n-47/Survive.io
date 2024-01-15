using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider m_slider;

    private void Awake()
    {
        m_slider = GetComponent<Slider>();
    }

    public void SetSliderPercentage(float percentage)
    {
        m_slider.value = percentage;
    }

    void Start()
    {
        //float width = Screen.width /1920.0f;
        //float height = Screen.height / 1080.0f;
        //m_rectTransform.localScale = new Vector3(width, width, 1.0f);
        //m_rectTransform.anchoredPosition = new Vector3(width * 300.0f, height * -40.0f, 0.0f);
    }
}
