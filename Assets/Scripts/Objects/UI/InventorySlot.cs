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

    [SerializeField] private TMPro.TextMeshProUGUI m_HotKeyText;
    public TMPro.TextMeshProUGUI HotKeyText
    {
        get { return m_HotKeyText; }
        set { m_HotKeyText = value; }
    }

    [SerializeField] private Sprite m_SlotUnselectedSprite;
    [SerializeField] private Sprite m_SlotSelectedSprite;

    [SerializeField] private TMPro.TextMeshProUGUI m_StoreValueText;

    private Color m_SlotTakenColor = new Color(1f, 1f, 1f, 1f);
    private Color m_SlotNotTakenColor = new Color(1f, 1f, 1f, 0f);
    private Color m_LightUpSlotColor = new Color(178 / 255f, 106f / 255f, 63 / 255f);

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

    public void FillSlot(ObjectData objectData, int amount)
    {
        m_ObjectData = objectData;
        m_SlotImage.color = m_SlotTakenColor;
        m_SlotImage.sprite = objectData.Icon;
        SetAmount(amount);
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

    public void ShowStoreValue(bool show)
    {
        m_StoreValueText.text = m_ObjectData.SellingCost.ToString();
        m_StoreValueText.gameObject.SetActive(show);
    }

    public void LightUpSlot(bool lightUp)
    {
        if (lightUp)
        {
            m_BackGroundImage.color = m_LightUpSlotColor;
        }
        else
        {
            m_BackGroundImage.color = m_SlotTakenColor;
        }
    }
}
