using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSpinAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject m_spinObject;
    [SerializeField]
    private float m_rotationSpeed = 5.0f;

    public void Awake()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        while(true)
        {
            float angle = m_spinObject.transform.rotation.eulerAngles.z + m_rotationSpeed * Time.deltaTime;

            Quaternion newAngle = Quaternion.Euler(0.0f, 0.0f, angle);

            m_spinObject.transform.rotation = newAngle;

            yield return null;
        }
    }
}
