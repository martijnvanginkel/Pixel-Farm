using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackSlot : MonoBehaviour
{
    private ObjectData m_ObjectData;
    public ObjectData ObjectData
    {
        get { return m_ObjectData; }
        set { m_ObjectData = value; }
    }

    private bool m_SlotIsTaken = false;
    public bool SlotIsTaken
    {
        get { return m_SlotIsTaken; }
        set { m_SlotIsTaken = value; }
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

    private bool m_IsSelected;
    public bool IsSelected
    {
        get { return m_IsSelected; }
        set { m_IsSelected = value; }
    }

    [SerializeField] private Image m_BackGroundImage;
    [SerializeField] private TMPro.TextMeshProUGUI m_SlotAmountText;
    [SerializeField] private GameObject m_DescriptionBox;
    [SerializeField] private TMPro.TextMeshProUGUI m_DescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        m_SlotImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SelectSlot(bool select)
    {
        if (select)
        {
            m_BackGroundImage.color = Color.red;
        }
        else
        {
            m_BackGroundImage.color = Color.white;
        }

    }
}
