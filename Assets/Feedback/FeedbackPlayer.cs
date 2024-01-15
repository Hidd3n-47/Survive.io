using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<IFeedback> m_feedbackToPlayer = null;

    public void PlayFeedback()
    {
        foreach (IFeedback feedback in m_feedbackToPlayer)
        {
            feedback.CompletePreviousFeedback();
            feedback.CreateFeedback();
        }
    }
}
