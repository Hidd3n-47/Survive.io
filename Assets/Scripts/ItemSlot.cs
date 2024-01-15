using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Image m_image;
    [SerializeField]
    GameObject m_selected;
    [SerializeField]
    GameObject m_equipped;
    
    public void SetItemSlotImage(Sprite image)
    {
        m_image.sprite = image;

        if(image == null)
        {
            m_image.gameObject.SetActive(false);
            return;
        }

        m_image.gameObject.SetActive(true);
    }

    public void EquipSlot()
    {
        m_equipped.SetActive(true);
    }

    public void UnequipSlot()
    {
        m_equipped.SetActive(false);
    }

    public void SelectSlot()
    {
        m_selected.SetActive(true);
    }

    public void DeselectSlot()
    { 
        m_selected.SetActive(false);
    }

    public void OnButtonPress()
    {
        SelectSlot();
        GunDisplay.Instance.SelectedSlot = this;
    }
}
