using UnityEngine;

public class AbilitySpeed : IAbility
{
    float[] m_multiplierValues = { 1.2f, 1.35f, 1.5f };
    int m_damageMuliplierLevel = 0;

    public override void ApplyAbility()
    {
        Debug.Log("Applied Speed");
        GameManagerSurvival.Instance.SpeedBoost = m_multiplierValues[m_damageMuliplierLevel++];

        if(m_damageMuliplierLevel >= m_multiplierValues.Length)
        {
            AbilityManager.Instance.RemoveAbility(this);
        }
    }

    public override Ability GetAbilityType()
    {
        return Ability.Speed;
    }
}
