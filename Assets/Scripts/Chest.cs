using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour
{
    Transform m_rewardUi;

    private void Awake()
    {
        m_rewardUi = GameObject.Find("UI Canvas").transform.GetChild(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }

        m_rewardUi.gameObject.SetActive(true);
        GunManager.Instance.UnlockGun(m_rewardUi.gameObject);
        //Time.timeScale = 0.0f;
    }
}
