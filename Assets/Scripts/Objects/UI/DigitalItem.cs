using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A DigitalItem is an item that is in the inventory or in the store. It represents an item but is not physically in the scene
public class DigitalItem : MonoBehaviour
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

    [SerializeField] private PlayerController m_Player;
    [SerializeField] private TMPro.TextMeshProUGUI m_SlotAmountText;

    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
        m_SelectedColor = new Color(1f, 1f, 1f, 1f);
        m_UnSelectedColor = new Color(1f, 1f, 1f, 0.25f);

        m_Player = FindObjectOfType<PlayerController>(); // this is very bad needs to be fixed
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

    public void BuyItemFromStore()
    {
        Store.Instance.BuyItem(this);
    }

    public void ItemClicked()
    {
        if (!Store.Instance.StoreIsOpen)
        {
            DropItemOnGround();
        }
        else
        {
            Store.Instance.SellItem(this);
        }
    }

    private void DropItemOnGround()
    {
        Inventory.Instance.RemoveItem(this, 1);

        GameObject tile = m_Player.FindStandingTile();

        float height = tile.GetComponent<Renderer>().bounds.size.y;


        Instantiate(ObjectData.Prefab, new Vector3(tile.transform.position.x, tile.transform.position.y + height / 2, tile.transform.position.z), transform.rotation);

       // Instantiate(m_ObjectData.Prefab, new Vector3(standingTile.transform.x, standingTile));
    }
}
