﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {

    }

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

    //private bool m_IsSelected;
    //public bool IsSelected
    //{
    //    get { return m_IsSelected; }
    //    set { m_IsSelected = value; }
    //}

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
