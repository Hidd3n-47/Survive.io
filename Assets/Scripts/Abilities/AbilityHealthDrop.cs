using UnityEngine;

public class AbilityHealthDrop : IAbility
{
    float[] m_healthDropPercentage = { 0.15f, 0.2f, 0.25f };
    int m_healthDropLevel = 0;

    public override void ApplyAbility()
    {
        Debug.Log("Applied Syphon");
        GameManagerSurvival.Instance.HealthDropPercentage = m_healthDropPercentage[m_healthDropLevel++];

        if (m_healthDropLevel >= m_healthDropPercentage.Length)
        {
            AbilityManager.Instance.RemoveAbility(this);
        }
    }

    public override Ability GetAbilityType()
    {
        return Ability.Syphon;
    }
}
