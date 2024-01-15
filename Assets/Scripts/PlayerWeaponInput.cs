using UnityEngine;
using UnityEngine.Events;


public class PlayerWeaponInput : MonoBehaviour
{
    [SerializeField]
    private Joystick m_joystick;
    [SerializeField]
    private GameObject m_weapon;

    private SpriteRenderer m_spriteRenderer;

    public UnityEvent OnShoot;
    public UnityEvent<Vector2> OnShootPosition;

    private void Start()
    {
        m_weapon = transform.GetChild(0).gameObject;
        m_spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = m_joystick.Direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.y > 0.0f)
        {
            m_spriteRenderer.sortingOrder = -1;
        }
        else
        {
            m_spriteRenderer.sortingOrder = 1;
        }

        if (direction.x < 0.0f)
        {
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }

        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = quaternion;

        if(direction != Vector2.zero)
        {
            if(m_weapon.GetComponent<IWeapon>().Shoot())
            {
                OnShoot?.Invoke();
                OnShootPosition?.Invoke((Vector2)(transform.parent.transform.position));
            }
        }
    }
}
