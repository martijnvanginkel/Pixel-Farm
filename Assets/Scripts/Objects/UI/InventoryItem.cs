using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private int m_SlotAmount;
    public int SlotAmount
    {
        get { return m_SlotAmount; }
        set { m_SlotAmount = value; }
    }

    private Image m_SlotImage;

    [SerializeField] private TMPro.TextMeshProUGUI m_SlotAmountText;

    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
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
}
