using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get { return m_Instance; }
    }

    // list met alleen tradeable objects en de counting op een andere manier
    //[SerializeField] private Dictionary<string, InventoryItem> m_InventoryDictionary = new Dictionary<string, InventoryItem>();
    //public Dictionary<string, InventoryItem> InventoryDictionary
    //{
    //    get { return m_InventoryDictionary; }
    //    set { m_InventoryDictionary = value; }
    //}

    [SerializeField] private List<InventoryItem> m_InventoryList = new List<InventoryItem>();
    public List<InventoryItem> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    [SerializeField] private GameObject m_InventoryItemPrefab;

    [SerializeField] private int m_SelectedItem;

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
        
    }

    private void Update()
    {
        CheckForScrolling();
    }

    private void CheckForScrolling()
    {
        if(m_InventoryList.Count > 0)
        {
            m_SelectedItem += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel"));
            m_SelectedItem = Mathf.Clamp(m_SelectedItem, 1, m_InventoryList.Count); //prevents value from exceeding specified range
        }
    }

    private void AddInventorySlot(TradeableObject tradeableObject, int amount)
    {
        // Spawn new slotPrefab (empty image that holds the actual icon and amount)
        GameObject slotPrefab = Instantiate(m_InventoryItemPrefab); 
        slotPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

        InventoryItem item = slotPrefab.GetComponent<InventoryItem>();
        item.Name = tradeableObject.ObjectName;
        item.SetAmount(amount);
        item.SetImage(tradeableObject.Icon);

        m_InventoryList.Add(item);
    }

    public void AddItem(TradeableObject tradeableObject, int amount)
    {
        if(m_InventoryList.Count == 0) // If inventory is empty always add the item
        {
            AddInventorySlot(tradeableObject, amount);
        }
        else // If inventory is not empty 
        {
            if (!ItemInList(tradeableObject)) // Add new inventory slot if its not already in the list
            {
                AddInventorySlot(tradeableObject, amount);
            }
        }
    }

    private void AddSlotAmount(InventoryItem item, int amount)
    {
        item.IncreaseAmount(amount);
    }

    private bool ItemInList(TradeableObject tradeableObject)
    {
        for (int i = 0; i < m_InventoryList.Count; i++)
        {
            if (m_InventoryList[i].Name == tradeableObject.ObjectName)
            {
                AddSlotAmount(m_InventoryList[i], 1); // Add one to the slot amount before returning true
                return true;
            }
        }
        return false;
    }

    //private void DecreaseSlotAmount(TradeableObject tradeableObject, int amount)
    //{
    //    InventoryItem invSlot = m_InventoryDictionary[tradeableObject.ObjectName];

    //    invSlot.IncreaseAmount(amount);
    //}

    public InventoryItem GetSelectedItem()
    {
        // This is bad, should be in a list form

        return m_InventoryList[m_SelectedItem];

    }
}
