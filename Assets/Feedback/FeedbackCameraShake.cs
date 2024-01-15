using UnityEngine;
using Cinemachine;
using System.Collections;

public class FeedbackCameraShake : IFeedback
{
    [SerializeField] 
    private CinemachineVirtualCamera m_cinCamera;
    [SerializeField]
    private float m_shakeAmplitude = 1.0f;
    [SerializeField]
    private float m_shakeIntensity = 1.0f;
    [SerializeField]
    private float m_shakeDuration = 0.1f;

    private CinemachineBasicMultiChannelPerlin m_noise = null;
     
    private void Awake()
    {
        m_noise = m_cinCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CompletePreviousFeedback()
    {
        StopAllCoroutines();
        m_noise.m_AmplitudeGain = 0.0f;
    }

    public override void CreateFeedback()
    {
        m_noise.m_AmplitudeGain = m_shakeAmplitude;
        m_noise.m_FrequencyGain = m_shakeIntensity;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float timer = m_shakeDuration;
        while(timer > 0.0f)
        {
            m_noise.m_AmplitudeGain = Mathf.Lerp(0.0f, m_shakeAmplitude, timer / m_shakeDuration);
            timer -= Time.deltaTime;
            yield return null;
        }
        //for (float i = m_shakeDuration; i > 0.0f; i -= Time.deltaTime)
        //{
        //    m_noise.m_AmplitudeGain = Mathf.Lerp(0, m_shakeAmplitude, i / m_shakeDuration);
        //    yield return null;
        //}

        m_noise.m_AmplitudeGain = 0.0f;
    }
}
