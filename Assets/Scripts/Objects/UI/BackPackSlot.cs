using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackSlot : DigitalItem
{
    //private ObjectData m_ObjectData;
    //public ObjectData ObjectData
    //{
    //    get { return m_ObjectData; }
    //    set { m_ObjectData = value; }
    //}

    //private bool m_SlotIsTaken = false;
    //public bool SlotIsTaken
    //{
    //    get { return m_SlotIsTaken; }
    //    set { m_SlotIsTaken = value; }
    //}

    //private int m_SlotAmount;
    //public int SlotAmount
    //{
    //    get { return m_SlotAmount; }
    //    set { m_SlotAmount = value; }
    //}

    //private Image m_SlotImage;
    //public Image SlotImage
    //{
    //    get { return m_SlotImage; }
    //    set { m_SlotImage = value; }
    //}

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

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SetAmount(int amount)
    //{
    //    m_SlotAmount = amount;

    //    if(m_SlotAmount == 0)
    //    {
    //        m_SlotAmountText.text = "";
    //    }
    //    else
    //    {
    //        m_SlotAmountText.text = m_SlotAmount.ToString();
    //    }
    //}

    //public void IncreaseAmount(int amount)
    //{
    //    m_SlotAmount += amount;
    //    m_SlotAmountText.text = m_SlotAmount.ToString();
    //}

    //public void DecreaseAmount(int amount)
    //{
    //    m_SlotAmount -= amount;
    //    m_SlotAmountText.text = m_SlotAmount.ToString();
    //}

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
