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

    // Start is called before the first frame update
    void Start()
    {
        SetStoreItems();
        
    }

    public void BuyItem(DigitalItem item)
    {
        if(item.SlotAmount > 1)
        {
            item.DecreaseAmount(1);
        }
        else
        {
            Destroy(item.gameObject);
        }

        Inventory.Instance.AddItem(item.ObjectData, 1);
        m_MoneyBar.LoseMoney(item.ObjectData);
    }

    public void OpenStorePanel()
    {
        SetInventoryItems();
        m_StorePanel.SetActive(true);
    }

    public void CloseStorePanel()
    {
        m_StorePanel.SetActive(false);
    }

    // Set the default items from the start of the game in the store
    private void SetStoreItems()
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

    private void SetInventoryItems()
    {
        foreach (DigitalItem item in Inventory.Instance.InventoryList)
        {
            // Spawn new itemPrefab
            GameObject itemPrefab = Instantiate(m_StoreItemPrefab);
            itemPrefab.transform.SetParent(m_InventoryItemsParent, false); // false so it scales locally

            item.SetAmount(item.SlotAmount);
            item.SetImage(item.ObjectData.Icon);
        }
    }
}
