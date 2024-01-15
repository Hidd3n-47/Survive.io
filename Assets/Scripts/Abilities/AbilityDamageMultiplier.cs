using UnityEngine;

public class AbilityDamageMultiplier : IAbility
{
    float[] m_multiplierValues = {1.2f, 1.6f, 2.0f };
    int m_damageMuliplierLevel = 0;

    public override void ApplyAbility()
    {
        Debug.Log("Applied Damage");
        GameManagerSurvival.Instance.DamageMultiplier = m_multiplierValues[m_damageMuliplierLevel++];

        if (m_damageMuliplierLevel >= m_multiplierValues.Length)
        {
            AbilityManager.Instance.RemoveAbility(this);
        }
    }

    public override Ability GetAbilityType()
    {
        return Ability.DamageMultiplier;
    }
}
