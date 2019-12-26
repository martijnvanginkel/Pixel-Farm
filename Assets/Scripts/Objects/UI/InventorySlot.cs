using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : DigitalItem
{
    private bool m_IsSelected;
    public bool IsSelected
    {
        get { return m_IsSelected; }
        set { m_IsSelected = value; }
    }

    public delegate void InvSlotClicked(InventorySlot inventorySlot);
    public static event InvSlotClicked OnInvSlotClicked;

    [SerializeField] private TMPro.TextMeshProUGUI m_HotKeyText;
    public TMPro.TextMeshProUGUI HotKeyText
    {
        get { return m_HotKeyText; }
        set { m_HotKeyText = value; }
    }

    [SerializeField] private Sprite m_SlotUnselectedSprite;
    [SerializeField] private Sprite m_SlotSelectedSprite;
    [SerializeField] private TMPro.TextMeshProUGUI m_StoreValueText;

    void Start()
    {
        m_SlotImage = GetComponent<Image>();
        m_SlotImage.color = m_SlotNotTakenColor;
    }

    public void SelectSlot(bool select)
    {
        if (select)
        {
            m_BackGroundImage.sprite = m_SlotSelectedSprite;
        }
        else
        {
            m_BackGroundImage.sprite = m_SlotUnselectedSprite;
        }
    }

    public override void FillSlot(ObjectData objectData, int amount)
    {
        base.FillSlot(objectData, amount);
        m_SlotImage.color = m_SlotTakenColor;
    }

    public override void ResetSlot()
    {
        base.ResetSlot();
        m_SlotImage.color = m_SlotNotTakenColor;
    }

    // If a player clicks on an inventory slot, check if the store is open or a compostbin before selecting
    public void ClickedOnSlot()
    {
        //if (Store.Instance.StoreIsOpen)
        //{
        //    if (m_SlotIsTaken)
        //    {
        //        Store.Instance.SellItem(this);
        //    }
        //}
        //if (GameManager.Instance.CompostUIIsOpen)
        //{
        //    if (m_SlotIsTaken)
        //    {
        //        GameManager.Instance.OpenCompostBin.AddItemToBin(this);
        //    }
        //}
        if (m_SlotIsTaken)
        {
            OnInvSlotClicked?.Invoke(this);
        }
        else
        {
            Inventory.Instance.SetSlotSelected(this);
        }
    }

    public void ShowStoreValue(bool show)
    {
        m_StoreValueText.text = m_ObjectData.SellingCost.ToString();
        m_StoreValueText.gameObject.SetActive(show);
    }
}
