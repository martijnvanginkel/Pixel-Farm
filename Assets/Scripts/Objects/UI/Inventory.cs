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
    [SerializeField] private Dictionary<string, InventoryItem> m_InventoryDictionary = new Dictionary<string, InventoryItem>();
    public Dictionary<string, InventoryItem> InventoryDictionary
    {
        get { return m_InventoryDictionary; }
        set { m_InventoryDictionary = value; }
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
        if(m_InventoryDictionary.Count > 0)
        {
            m_SelectedItem += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel"));
            m_SelectedItem = Mathf.Clamp(m_SelectedItem, 1, m_InventoryDictionary.Count); //prevents value from exceeding specified range
        }
    }

    private void AddInventorySlot(TradeableObject tradeableObject, int amount)
    {
        // Spawn new slotPrefab (empty image that holds the actual icon and amount)
        GameObject slotPrefab = Instantiate(m_InventoryItemPrefab); 
        slotPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

        InventoryItem item = slotPrefab.GetComponent<InventoryItem>();
        item.SetAmount(amount);
        item.SetImage(tradeableObject.Icon);

        InventoryDictionary.Add(tradeableObject.ObjectName, item);
    }

    public void AddItem(TradeableObject tradeableObject, int amount)
    {
        if(m_InventoryDictionary.Count == 0) // If inventory is empty always add the item
        {
            AddInventorySlot(tradeableObject, amount);
        }
        else
        {
            if (m_InventoryDictionary.ContainsKey(tradeableObject.ObjectName)) // If item is already in the inventory only change the amount
            {
                AddSlotAmount(tradeableObject, amount);
            }
            else
            {
                AddInventorySlot(tradeableObject, amount);
            }
        }
    }

    private void AddSlotAmount(TradeableObject tradeableObject, int amount)
    {
        InventoryItem invSlot = m_InventoryDictionary[tradeableObject.ObjectName];

        invSlot.IncreaseAmount(amount);
    }

    private void DecreaseSlotAmount(TradeableObject tradeableObject, int amount)
    {
        InventoryItem invSlot = m_InventoryDictionary[tradeableObject.ObjectName];

        invSlot.IncreaseAmount(amount);
    }

    public void GetSelectedItem()
    {
        // This is bad, should be in a list form

    }
}
