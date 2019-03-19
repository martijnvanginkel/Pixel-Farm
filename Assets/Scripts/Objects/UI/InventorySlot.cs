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

    //[SerializeField] private Sprite m_SlotEmptySprite;
    [SerializeField] private Sprite m_SlotUnselectedSprite;
    [SerializeField] private Sprite m_SlotSelectedSprite;

    private Color m_SlotTakenColor = new Color(1f, 1f, 1f, 1f);
    private Color m_SlotNotTakenColor = new Color(1f, 1f, 1f, 0f);

    // Start is called before the first frame update
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

    public void FillSlot(ObjectData objectData)
    {
        m_ObjectData = objectData;
        m_SlotImage.color = m_SlotTakenColor;
        m_SlotImage.sprite = objectData.Icon;
        SetAmount(1);
        m_SlotIsTaken = true;
    }

    public void ResetSlot()
    {
        m_ObjectData = null;
        m_SlotImage.color = m_SlotNotTakenColor;
        m_SlotIsTaken = false;
        SetAmount(0);
    }

    public void ClickedOnSlot()
    {
        if (!Store.Instance.StoreIsOpen)
        {
            Debug.Log("hoi");
            Inventory.Instance.SetSlotSelected(this);
        }
        else
        {
            if (m_SlotIsTaken)
            {
                Store.Instance.SellItem(this);
            }
        }
    }
}
