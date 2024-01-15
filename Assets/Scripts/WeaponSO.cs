using UnityEngine;

[CreateAssetMenu(fileName = "Weapon/WeaponStats", menuName = "Weapon/WeaponStats")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField]
    public Transform GunPrefab {  get; private set; }

    // Bullets per second.
    [field: SerializeField]
    public float FireRate { get; private set; } = 5;

    // How many bullets per shot.
    [field: SerializeField]
    public int BulletsPerShot { get; private set; } = 1;

    // Spread of the bullets in degrees.
    [field: SerializeField]
    [field: Range(0.0f, 90.0f)]
    public float BulletSpawnSpread { get; private set; } = 5.0f;

    // The distance before bullet is destroyed.
    [field: SerializeField]
    public float BulletDespawnDistance { get; private set; } = 4.0f;

    // Audio clip of the gun shooting effect.
    [field: SerializeField]
    public AudioClip ShootSoundEffect { get; private set; } = null;

    // Bullet associated with the gun.
    [field: SerializeField]
    public BulletDataSO Bullet { get; private set; } = null;

    public float Damage { get { return Bullet.Damage; } }
    public float CritRate { get { return Bullet.CritRate; } }
}
