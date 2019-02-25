using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private ObjectData m_ObjectData;
    public ObjectData ObjectData
    {
        get { return m_ObjectData; }
        set { m_ObjectData = value; }
    }

    private int m_SlotAmount;
    public int SlotAmount
    {
        get { return m_SlotAmount; }
        set { m_SlotAmount = value; }
    }

    private Image m_SlotImage;
    public Image SlotImage
    {
        get { return m_SlotImage; }
        set { m_SlotImage = value; }
    }

    private Color m_SelectedColor;
    private Color m_UnSelectedColor;

    [SerializeField] private TMPro.TextMeshProUGUI m_SlotAmountText;

    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
        m_SelectedColor = new Color(1f, 1f, 1f, 1f);
        m_UnSelectedColor = new Color(1f, 1f, 1f, 0.5f);
    }

    // Makes the selected inventory item more bright
    public void SetItemSelected()
    {
        m_SlotImage.color = m_SelectedColor;
    }

    // Makes an unselected inventory item less bright
    public void SetItemUnselected()
    {
        m_SlotImage.color = m_UnSelectedColor;
    }

    public void SetImage(Sprite image)
    {
        m_SlotImage.sprite = image;
    }

    public void SetAmount(int amount)
    {
        m_SlotAmount = amount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }

    public void IncreaseAmount(int amount)
    {
        m_SlotAmount += amount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }

    public void DecreaseAmount(int amount)
    {
        m_SlotAmount -= amount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }
}
