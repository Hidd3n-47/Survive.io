using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovementInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float m_speed = 5.0f;
    [SerializeField]
    private float m_dashCooldown = 1.5f;
    [SerializeField]
    private float m_dashImpulseMagnitude = 6.0f;
    [SerializeField]
    Joystick m_joystick;

    private bool m_dash = false;
    private bool m_canDash = true;

    private int m_idleAnimation = Animator.StringToHash("PlayerIdle");
    private int m_movementAnimation = Animator.StringToHash("PlayerMovement");

    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private AudioSource m_audio;
    private Player m_player;

    private LayerMask m_wallLayer;

    [SerializeField]
    Transform test;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("touch");
        test.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("release");
        test.gameObject.SetActive(true);
    }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
        m_player = GetComponent<Player>();

        m_wallLayer = LayerMask.GetMask("Walls");
    }

    private void FixedUpdate()
    {
        Vector2 direction = m_joystick.Direction;
       
        if (direction == Vector2.zero)
        {
            m_animator.CrossFade(m_idleAnimation, 0, 0);
            return;
        }

        m_animator.CrossFade(m_movementAnimation, 0, 0);

        m_spriteRenderer.flipX = direction.x < 0.0f;

        float speedBoost = GameManagerSurvival.Instance ? GameManagerSurvival.Instance.SpeedBoost : 1.0f;

        m_rigidbody.velocity = direction * m_speed * speedBoost;

        if(m_dash || Input.GetKeyDown(KeyCode.Space))
        {
            DashCalculation();
            m_dash = false;
        }
    }

    private void DashCalculation()
    {
        if(!m_canDash)
        {
            return;
        }

        Vector2 dir = m_joystick.Direction.normalized;

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, 2.0f, m_wallLayer);

        if(raycast.collider != null)
        {
            return;
        }

        m_rigidbody.AddForce(dir * m_dashImpulseMagnitude, ForceMode2D.Impulse);

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        m_canDash = false;
        m_player.ActivateIFrames();
        yield return new WaitForSeconds(m_dashCooldown);
        m_canDash = true;
        Debug.Log("CanDash");
    }

    public void PlayFootstepAudio()
    {
        m_audio.Play();
    }

    public void Dash()
    {
        m_dash = true;
    }
}
