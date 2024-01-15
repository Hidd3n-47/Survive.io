using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IWeapon : MonoBehaviour
{
    [SerializeField]
    WeaponSO m_weaponData;
    [SerializeField]
    Transform m_muzzle;

    public WeaponSO WeaponData {  get { return m_weaponData; } }

    private AudioSource m_audioSource;

    private bool m_canShoot = true;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.clip = m_weaponData.ShootSoundEffect;
    }

    public bool Shoot()
    {
        if(m_canShoot)
        {
            for(int i = 0; i < m_weaponData.BulletsPerShot; i++)
            {
                SingleShot();
            }

            StartCoroutine(StartShootCooldown());
            return true;
        }
        return false;
    }

    private void SingleShot()
    {
        float spread = Random.Range(-m_weaponData.BulletSpawnSpread, m_weaponData.BulletSpawnSpread);

        Quaternion bulletRotation = Quaternion.Euler(0.0f, 0.0f, spread - 90.0f) * m_muzzle.rotation;

        Transform bullet = Instantiate(m_weaponData.Bullet.BulletPrefab, m_muzzle.position, bulletRotation);
        Bullet b = bullet.GetComponent<Bullet>();

        b.InstantiateBullet(bulletRotation * Vector3.up, m_weaponData.Bullet);
    }
    private IEnumerator StartShootCooldown()
    {
        m_canShoot = false;
        m_audioSource.Play();
        yield return new WaitForSeconds(1.0f / m_weaponData.FireRate);
        m_canShoot = true;
    }
}
