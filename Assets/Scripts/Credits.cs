using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void OnCreditsPressed()
    {
        gameObject.SetActive(true);
    }

    public void OnXPressed()
    {
        gameObject.SetActive(false);
    }
}
