using UnityEngine;

public class AbilityDefense : IAbility
{
    float[] m_multiplierValues = { 0.9f, 0.85f, 0.8f };
    int m_damageMuliplierLevel = 0;

    public override void ApplyAbility()
    {
        Debug.Log("Applied Defense");
        GameManagerSurvival.Instance.Defense = m_multiplierValues[m_damageMuliplierLevel++];

        if (m_damageMuliplierLevel >= m_multiplierValues.Length)
        {
            AbilityManager.Instance.RemoveAbility(this);
        }
    }

    public override Ability GetAbilityType()
    {
        return Ability.Defense;
    }
}
