using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for storeslots and inventory slots
public abstract class DigitalItem : MonoBehaviour
{
    protected ObjectData m_ObjectData;
    public ObjectData ObjectData
    {
        get { return m_ObjectData; }
        set { m_ObjectData = value; }
    }

    protected bool m_SlotIsTaken = false;
    public bool SlotIsTaken
    {
        get { return m_SlotIsTaken; }
        set { m_SlotIsTaken = value; }
    }

    protected int m_SlotAmount;
    public int SlotAmount
    {
        get { return m_SlotAmount; }
        set { m_SlotAmount = value; }
    }

    protected Image m_SlotImage;
    public Image SlotImage
    {
        get { return m_SlotImage; }
        set { m_SlotImage = value; }
    }

    [SerializeField] protected Image m_BackGroundImage;
    [SerializeField] protected TMPro.TextMeshProUGUI m_SlotAmountText;
    [SerializeField] protected GameObject m_DescriptionBox;
    [SerializeField] protected TMPro.TextMeshProUGUI m_DescriptionText;

    public void SetImage(Sprite image)
    {
        m_SlotImage.sprite = image;
    }

    public void SetAmount(int amount)
    {
        m_SlotAmount = amount;

        if(m_SlotAmount == 0)
        {
            m_SlotAmountText.text = "";
        }
        else
        {
            m_SlotAmountText.text = m_SlotAmount.ToString();
        }
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
