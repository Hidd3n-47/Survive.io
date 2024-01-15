using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunDisplay : MonoBehaviour
{
    public static GunDisplay Instance;

    [SerializeField]
    private GameObject m_slotPanel;

    [SerializeField]
    private GameObject m_equippedPanel;
    [SerializeField]
    private GameObject m_selectedPanel;

    [SerializeField]
    private Color m_higherStat;
    [SerializeField]
    private Color m_lowerStat;

    private ItemSlot m_selectedSlot;
    private ItemSlot m_equippedSlot;
    int m_selectedIndex = -1;

    private List<ItemSlot> m_weaponSlots = new();

    public ItemSlot SelectedSlot 
    { 
        get { return m_selectedSlot; } 
        set 
        { 
            m_selectedSlot?.DeselectSlot();

            m_selectedSlot = value; 

            if(m_selectedSlot == m_equippedSlot)
            {
                ResetSelectedStats();
                m_selectedSlot.DeselectSlot();
                m_selectedSlot = null;
                return;
            }

            int numWeapons = PlayerStats.Instance.GetNextGunIndex();
            for(int i = 0; i < numWeapons; i++)
            {
                if (m_weaponSlots[i] == m_selectedSlot)
                {
                    m_selectedIndex = i;
                    SetSelectedStats(i);
                    return;
                }
            }

            m_selectedSlot.DeselectSlot();
            ResetSelectedStats();
            m_selectedSlot = null;
        } 
    }
    public ItemSlot EquippedSlot { get { return m_equippedSlot; } set { m_equippedSlot?.UnequipSlot(); m_equippedSlot = value; } }

    public void OnEquipPress()
    {
        if (!m_selectedSlot) return;

        Debug.Assert(m_selectedIndex != -1);

        int index = m_selectedIndex;

        PlayerStats.Instance.SetEquipped(index);

        ResetSelectedStats();
        m_selectedSlot.DeselectSlot();
        m_equippedSlot.UnequipSlot();
        m_equippedSlot = m_selectedSlot;
        m_equippedSlot.EquipSlot();
        m_selectedSlot = null;

        SetEquippedStats(index);
    }

    public void DisplayGuns()
    {
        int index = PlayerStats.Instance.GetNextGunIndex();
        int equipped = PlayerStats.Instance.GetEquipped();

        for(int i = 0; i < index; i++)
        {
            m_weaponSlots[i].SetItemSlotImage(GunManager.Instance.GetSprite(i));


            if(i == equipped)
            {
                m_equippedSlot = m_weaponSlots[i];
                m_weaponSlots[i].EquipSlot();
                SetEquippedStats(i);
            }
        }
    }

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i < m_slotPanel.transform.childCount; i++)
        {
            m_weaponSlots.Add(m_slotPanel.transform.GetChild(i).GetComponent<ItemSlot>());
        }
    }

    private void SetEquippedStats(int gunIndex)
    {
        WeaponSO stats = GunManager.Instance.GetGunStats(gunIndex);
        // FireRate
        // Damage
        // Crit Chance.
        // Bullets Per Shot.

        Transform textChildren = m_equippedPanel.transform.GetChild(0);

        textChildren.GetChild(0).GetComponent<TextMeshProUGUI>().text = stats.FireRate.ToString();
        textChildren.GetChild(1).GetComponent<TextMeshProUGUI>().text = stats.Damage.ToString();
        textChildren.GetChild(2).GetComponent<TextMeshProUGUI>().text = stats.CritRate.ToString();
        textChildren.GetChild(3).GetComponent<TextMeshProUGUI>().text = "1"; // To be implemented.
    }

    private void SetSelectedStats(int gunIndex)
    {
        WeaponSO stats = GunManager.Instance.GetGunStats(gunIndex);
        WeaponSO equippedStats = GunManager.Instance.GetGunStats(PlayerStats.Instance.GetEquipped());
        // FireRate
        // Damage
        // Crit Chance.
        // Bullets Per Shot.

        bool oneHigher;
        bool twoHigher;
        bool thrHigher;

        Transform textChildren = m_selectedPanel.transform.GetChild(0);

        TextMeshProUGUI one = textChildren.GetChild(0).GetComponent<TextMeshProUGUI>();
        one.text = stats.FireRate.ToString();
        oneHigher = stats.FireRate > equippedStats.FireRate;
        one.color = oneHigher ? m_higherStat : m_lowerStat;

        TextMeshProUGUI two = textChildren.GetChild(1).GetComponent<TextMeshProUGUI>();
        two.text = stats.Damage.ToString();
        twoHigher = stats.Damage > equippedStats.Damage;
        two.color = twoHigher ? m_higherStat : m_lowerStat;

        TextMeshProUGUI thr = textChildren.GetChild(2).GetComponent<TextMeshProUGUI>();
        thr.text = stats.CritRate.ToString();
        thrHigher = stats.CritRate > equippedStats.CritRate;
        thr.color = thrHigher ? m_higherStat : m_lowerStat;

        TextMeshProUGUI fou = textChildren.GetChild(3).GetComponent<TextMeshProUGUI>();
        fou.text = "1"; // To be implemented.
        fou.color = m_lowerStat;

        textChildren = m_equippedPanel.transform.GetChild(0);

        textChildren.GetChild(0).GetComponent<TextMeshProUGUI>().color = oneHigher ? m_lowerStat : m_higherStat;
        textChildren.GetChild(1).GetComponent<TextMeshProUGUI>().color = twoHigher ? m_lowerStat : m_higherStat;
        textChildren.GetChild(2).GetComponent<TextMeshProUGUI>().color = thrHigher ? m_lowerStat : m_higherStat;
        textChildren.GetChild(3).GetComponent<TextMeshProUGUI>().color = m_higherStat;
    }

    private void ResetSelectedStats()
    {
        if(!m_selectedSlot) return;

        Transform textChildren = m_selectedPanel.transform.GetChild(0);

        TextMeshProUGUI one = textChildren.GetChild(0).GetComponent<TextMeshProUGUI>();
        one.text = "-";
        one.color = Color.white;

        TextMeshProUGUI two = textChildren.GetChild(1).GetComponent<TextMeshProUGUI>();
        two.text = "-";
        two.color = Color.white;

        TextMeshProUGUI thr = textChildren.GetChild(2).GetComponent<TextMeshProUGUI>();
        thr.text = "-";
        thr.color = Color.white;

        TextMeshProUGUI fou = textChildren.GetChild(3).GetComponent<TextMeshProUGUI>();
        fou.text = "-"; 
        fou.color = Color.white;

        textChildren = m_equippedPanel.transform.GetChild(0);

        textChildren.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        textChildren.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
        textChildren.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.white;
        textChildren.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.white;

        m_selectedIndex = -1;
    }
}
