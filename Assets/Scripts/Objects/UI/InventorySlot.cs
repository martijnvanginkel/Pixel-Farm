using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Image m_SlotImage;
    public Image SlotImage
    {
        get { return m_SlotImage; }
        set { m_SlotImage = value; }
    }

    private int m_SlotAmount;
    public int SlotAmount
    {
        get { return m_SlotAmount; }
        set { m_SlotAmount = value; }
    }

    [SerializeField] private TMPro.TextMeshProUGUI m_SlotAmountText;
    public TMPro.TextMeshProUGUI SlotAmountText
    {
        get { return m_SlotAmountText; }
        set { m_SlotAmountText = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
    }

    public void SetImage(Sprite slotSprite)
    {
        m_SlotImage.sprite = slotSprite;
        //Debug.Log(slotSprite.name);
        //Debug.Log(m_SlotImage);
        //m_SlotImage.sprite = slotSprite;
    }

    public void SetAmount(int increaseAmount)
    {
        m_SlotAmount += increaseAmount;
        m_SlotAmountText.text = m_SlotAmount.ToString();
    }

}
