using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIAnimator : MonoBehaviour
{
    [SerializeField]
    private float m_durationBetweenFrames = 0.1f;
    [SerializeField]
    private List<Sprite> m_sprites = new();

    private Image m_image;

    private float m_timer = 0.0f;
    private int m_spriteIndex;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer > m_durationBetweenFrames)
        {
            m_timer = 0.0f;
            
            m_image.sprite = m_sprites[m_spriteIndex];

            m_spriteIndex = (m_spriteIndex + 1) % (m_sprites.Count - 1);
        }
    }
}
