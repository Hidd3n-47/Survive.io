using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), RequireComponent(typeof(Button))]
public class UiButton : MonoBehaviour
{
    [SerializeField]
    Sprite m_buttonUp;
    [SerializeField]
    Sprite m_buttonDown;
    [SerializeField]
    Vector2 m_textMoveOnPress;

    Image m_image;
    RectTransform m_rectTransform;

    public void OnButtonDown()
    {
        m_image.sprite = m_buttonDown;

        if(m_rectTransform)
        {
            m_rectTransform.anchoredPosition += m_textMoveOnPress;
        }
    }

    public void OnButtonUp() 
    {
        m_image.sprite = m_buttonUp;

        if (m_rectTransform)
        {
            m_rectTransform.anchoredPosition -= m_textMoveOnPress;
        }
    }

    private void Awake()
    {
        m_image = GetComponent<Image>();

        if(transform.childCount > 0)
        {
            m_rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        }
    }
}
