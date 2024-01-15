using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance;

    [SerializeField]
    private List<IWeapon> m_guns;
    [SerializeField]
    private List<Sprite> m_sprites;

    public Transform GetPrefab(int index)
    {
        return m_guns[index].WeaponData.GunPrefab;
    }

    public Sprite GetSprite(int index)
    {
        return m_sprites[index];
    }

    public WeaponSO GetGunStats(int index)
    {
        return m_guns[index].WeaponData;
    }

    public void UnlockGun(GameObject gunPanel)
    {
        int index = PlayerStats.Instance.GetNextGunIndex();

        if (index >= m_guns.Count)
        {
            return;
        }

        gunPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = m_sprites[index];

        PlayerStats.Instance.UnlockGun(index);
    }

    public int GetNumberOfGuns()
    {
        return m_guns.Count;
    }

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);
    }
}
