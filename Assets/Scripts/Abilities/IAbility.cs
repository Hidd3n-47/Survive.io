using UnityEngine;

[System.Serializable]
public enum Ability
{ 
    DamageMultiplier,
    Syphon,
    Speed,
    Defense
}


public abstract class IAbility
{
    public abstract void ApplyAbility();
    public abstract Ability GetAbilityType();
}
