using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private static Store m_Instance;
    public static Store Instance
    {
        get { return m_Instance; }
    }

    [SerializeField] private List<ObjectData> m_StartObjects = new List<ObjectData>();
    public List<ObjectData> StartObjects
    {
        get { return m_StartObjects; }
        set { m_StartObjects = value; }
    }

    private List<DigitalItem> m_StoreItemList = new List<DigitalItem>();
    public List<DigitalItem> StoreItemList    
    {
        get { return m_StoreItemList; }
        set { m_StoreItemList = value; }
    }

    private bool m_StoreIsOpen = false;
    public bool StoreIsOpen
    {
        get { return m_StoreIsOpen; }
        set { m_StoreIsOpen = value; }
    }

    [SerializeField] private GameObject m_StorePanel;
    [SerializeField] private Transform m_StoreItemsParent;
    [SerializeField] private Transform m_InventoryItemsParent;
    [SerializeField] private GameObject m_StoreItemPrefab;
    [SerializeField] private MoneyBar m_MoneyBar;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    void Start()
    {
        SetDefaultItems();
    }

    public void OpenStorePanel()
    {
        m_StorePanel.SetActive(true);
        m_StoreIsOpen = true;
        PlayerController.Instance.AllowInput = false;
    }

    public void CloseStorePanel()
    {
        m_StorePanel.SetActive(false);
        m_StoreIsOpen = false;
        PlayerController.Instance.AllowInput = true;
    }

    public void BuyItem(DigitalItem item)
    {
        if(item.SlotAmount > 1)
        {
            item.DecreaseAmount(1);
            Inventory.Instance.AddItem(item.ObjectData, 1);
            m_MoneyBar.LoseMoney(item.ObjectData);
        }
        else
        {
            Inventory.Instance.AddItem(item.ObjectData, 1);
            m_MoneyBar.LoseMoney(item.ObjectData);
            RemoveSlot(item);
        }
    }

    public void SellItem(DigitalItem item)
    {
        m_MoneyBar.GainMoney(item.ObjectData);
        AddItemToStore(item.ObjectData, 1);
        Inventory.Instance.RemoveItem(item, 1);
    }

    private void AddItemToStore(ObjectData objectData, int amount)
    {
        if (m_StoreItemList.Count == 0) // If the store is empty always add the item
        {
            AddStoreSlot(objectData, amount);
        }
        else // If the store is not empty
        {
            if (!ItemInStore(objectData)) // Add new store slot if its not in the store already
            {
                AddStoreSlot(objectData, amount);
            }
        }
    }

    private void AddStoreSlot(ObjectData objectData, int amount)
    {
        GameObject itemPrefab = Instantiate(m_StoreItemPrefab);
        itemPrefab.transform.SetParent(m_StoreItemsParent, false); // false so it scales locally

        DigitalItem item = itemPrefab.GetComponent<DigitalItem>();
        item.ObjectData = objectData;
        item.SetAmount(amount);
        item.SetImage(objectData.Icon);

        m_StoreItemList.Add(item);
    }

    private void RemoveSlot(DigitalItem item)
    {
        m_StoreItemList.Remove(item);
        Destroy(item.gameObject);
    }

    // Return a true or false value to check if the store
    private bool ItemInStore(ObjectData objectData)
    {
        for (int i = 0; i < m_StoreItemList.Count; i++)
        {
            if (m_StoreItemList[i].ObjectData.Name == objectData.Name)
            {
                AddSlotAmount(m_StoreItemList[i], 1);
                return true;
            }
        }
        return false;
    }

    // Add slotamount to a slot that already exists
    private void AddSlotAmount(DigitalItem item, int amount)
    {
        item.IncreaseAmount(amount);
    }

    // Decrease slotamount to a slot that already exists
    private void RemoveSlotAmount(DigitalItem item, int amount)
    {
        item.DecreaseAmount(amount);
    }

    // Set the default items from the start of the game in the store
    private void SetDefaultItems()
    {
        foreach (ObjectData objectData in m_StartObjects)
        {
            // Spawn new itemPrefab
            GameObject itemPrefab = Instantiate(m_StoreItemPrefab);
            itemPrefab.transform.SetParent(m_StoreItemsParent, false); // false so it scales locally

            DigitalItem item = itemPrefab.GetComponent<DigitalItem>();
            item.ObjectData = objectData;
            item.SetAmount(objectData.DefaultStoreAmount);
            item.SetImage(objectData.Icon);

            m_StoreItemList.Add(item);
        }
    }
}
