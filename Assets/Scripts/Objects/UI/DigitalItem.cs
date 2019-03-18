using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// A DigitalItem is an item that is in the inventory or in the store. It represents an item but is not physically in the scene
public class DigitalItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] private GameObject m_DescriptionBox;
    [SerializeField] private TMPro.TextMeshProUGUI m_DescriptionText;

    private bool m_MouseIsOver;

    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
        m_SelectedColor = new Color(1f, 1f, 1f, 1f);
        m_UnSelectedColor = new Color(1f, 1f, 1f, 0.25f);
    }

    void Start()
    {
        //SetObjectDescription();
    }

    void SetObjectDescription()
    {
        if(ObjectData.Description != null)
        {
            m_DescriptionText.text = ObjectData.Description;
        }
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
            //Inventory.Instance.SetClickedItemSelected(this);
            DropItemOnGround();
        }
        else
        {
            Store.Instance.SellItem(this);
        }
    }

    private void DropItemOnGround()
    {
        GameObject tile = PlayerController.Instance.FindStandingTile();
        float height = tile.GetComponent<Renderer>().bounds.size.y;

        Instantiate(ObjectData.Prefab, new Vector3(PlayerController.Instance.GetPlayerPosition().position.x, tile.transform.position.y + height / 2, tile.transform.position.z), transform.rotation);
        Inventory.Instance.RemoveItem(this, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_MouseIsOver = true;
        StartCoroutine("WaitToShowDescription");
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_MouseIsOver = false;
        ShowDescription(false);
        Debug.Log("Mouse exit");
    }

    private IEnumerator WaitToShowDescription()
    {
        yield return new WaitForSeconds(1f);
        if (m_MouseIsOver)
        {
            ShowDescription(true);
        }
    }

    private void ShowDescription(bool isOn)
    {
        m_DescriptionBox.SetActive(isOn);
        Debug.Log("show description");
    }
}
