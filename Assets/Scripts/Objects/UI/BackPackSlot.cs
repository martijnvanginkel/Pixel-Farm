using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackSlot : DigitalItem
{

    private bool m_IsSelected;
    public bool IsSelected
    {
        get { return m_IsSelected; }
        set { m_IsSelected = value; }
    }

    [SerializeField] private Sprite m_SlotEmptySprite;
    [SerializeField] private Sprite m_SlotAvailableBorder;
    [SerializeField] private Sprite m_SlotTakenBorder;

    // Start is called before the first frame update
    void Start()
    {
        m_SlotImage = GetComponent<Image>();
        m_SlotImage.sprite = m_SlotEmptySprite;
    }

    public void SelectSlot(bool select)
    {
        if (select)
        {
            m_BackGroundImage.sprite = m_SlotTakenBorder;
        }
        else
        {
            m_BackGroundImage.sprite = m_SlotAvailableBorder;
        }

    }

    public void ResetSlot()
    {
        m_ObjectData = null;
        m_SlotImage.sprite = m_SlotEmptySprite;
        m_SlotIsTaken = false;
        SetAmount(0);
    }

    public void ClickedOnSlot()
    {
        if (!Store.Instance.StoreIsOpen)
        {
            BackPack.Instance.SetClickedSlotSelected(this);
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
