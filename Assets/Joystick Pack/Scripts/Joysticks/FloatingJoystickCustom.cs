using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FloatingJoystickCustom : Joystick
{
    [SerializeField]
    private float m_minTime = 0.1f;
    private float m_timer;
    private bool m_activated;

    private PlayerMovementInput m_movementInput;

    protected override void Start()
    {
        m_movementInput = GameObject.Find("Player").GetComponent<PlayerMovementInput>();

        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        m_timer = 0.0f;
        m_activated = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        m_timer += Time.deltaTime;
        if(m_timer > m_minTime)
        {
            if(!m_activated)
            {
                ActivateJoystick(eventData);
            }
            base.OnDrag(eventData);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(m_timer < m_minTime)
        {
            m_movementInput.Dash();
        }

        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    private void ActivateJoystick(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        m_activated = true;
    }
}