using UnityEngine;

[CreateAssetMenu(fileName = "Weapon/BulletData", menuName = "Weapon/BulletData")]
public class BulletDataSO : ScriptableObject
{
    // Prefab of the bullet.
    [field: SerializeField]
    public Transform BulletPrefab { get; private set; } = null;

    // The speed of the bullet.
    [field: SerializeField]
    public float BulletSpeed { get; private set; } = 5.0f;

    // Number of hittable objects it can go through before gettind destroyed.
    [field: SerializeField]
    public int BulletPenetration { get; private set; } = 1;

    // The damage of the bullet.
    [field: SerializeField]
    public float Damage { get; private set; } = 0.0f;

    [field: SerializeField]
    public float CritRate { get; private set; } = 0.0f;

}
