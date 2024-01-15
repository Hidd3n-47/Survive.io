using UnityEngine;

[CreateAssetMenu(fileName = "Enemy/EnemyData", menuName = "Enemy/EnemyData")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField]
    public float Health { get; private set; } = 20.0f;
    [field: SerializeField]
    public float Damage { get; private set; } = 5.0f;
    [field: SerializeField]
    public float Speed { get; private set; } = 3.0f;
    [field: SerializeField]
    public AudioClip DamageSound { get; private set; } = null;
    [field: SerializeField]
    public AudioClip DeathSound { get; private set; } = null;
}
