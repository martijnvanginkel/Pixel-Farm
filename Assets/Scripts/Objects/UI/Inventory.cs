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

    [SerializeField] private List<InventoryItem> m_InventoryList = new List<InventoryItem>();
    public List<InventoryItem> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    [SerializeField] private GameObject m_InventoryItemPrefab;

    [SerializeField] private int m_SelectedItem;
    [SerializeField] private int m_LastSelectedItem;
    private float m_ScrollValue; 

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

    // Function that check if the player is scrolling selects current item
    private void CheckForScrolling()
    {
        if(m_InventoryList.Count > 0)
        {
            m_ScrollValue += Input.GetAxis("Mouse ScrollWheel");
            m_ScrollValue = Mathf.Clamp(m_ScrollValue, 0f, (float)(m_InventoryList.Count - 1));
            m_SelectedItem = Mathf.RoundToInt(m_ScrollValue); // Prevents value from exceeding specified range

            if(m_SelectedItem != m_LastSelectedItem)
            {
                SetSelectedItemColor();
            }
        }
    }

    private void SetSelectedItemColor()
    {
        m_InventoryList[m_SelectedItem].SetItemSelected(); ;
        m_InventoryList[m_LastSelectedItem].SetItemUnselected();

        m_LastSelectedItem = m_SelectedItem;
    }

    public void AddItem(ObjectData objectData, int amount)
    {
        if(m_InventoryList.Count == 0) // If inventory is empty always add the item
        {
            AddInventorySlot(objectData, amount);
            m_InventoryList[m_SelectedItem].SetItemSelected();
        }
        else // If inventory is not empty 
        {
            if (!ItemInList(objectData)) // Add new inventory slot if its not in the list
            {
                AddInventorySlot(objectData, amount);
            }
        }
    }

    // Function that gets called first when an item is being received
    public void RemoveItem(InventoryItem item, int amount)
    {
        if(item.SlotAmount > 1) // If it has more than one, decrease the amount
        {
            RemoveSlotAmount(item, amount);
        }
        else // If it has one, remove the inventoryslot with the item in it
        {
            // Bepaal hier bij het uitgaan welke kant die op moet

            m_SelectedItem--; // moet anders
            m_LastSelectedItem--; // moet anders
            m_InventoryList[m_SelectedItem].SetItemSelected(); 
       
            RemoveInventorySlot(item);
        }
    }

    // Add a new inventory slot for an item that is not in the inventory
    private void AddInventorySlot(ObjectData objectData, int amount)
    {
        // Spawn new itemPrefab
        GameObject itemPrefab = Instantiate(m_InventoryItemPrefab);
        itemPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

        InventoryItem item = itemPrefab.GetComponent<InventoryItem>();
        item.ObjectData = objectData;
        item.SetAmount(amount);
        item.SetImage(objectData.Icon);

        m_InventoryList.Add(item);
    }

    // Remove an inventory slot 
    private void RemoveInventorySlot(InventoryItem item)
    {
        m_InventoryList.Remove(item);

        if (m_InventoryList.Count > 0)  //WELKE KANT MOET DE SELECTED OP
        {
            m_SelectedItem--;
        }

        Destroy(item.gameObject);
    }

    // Add slotamount to a slot that already exists
    private void AddSlotAmount(InventoryItem item, int amount)
    {
        item.IncreaseAmount(amount);
    }

    // Decrease slotamount to a slot that already exists
    private void RemoveSlotAmount(InventoryItem item, int amount)
    {
        item.DecreaseAmount(amount);
    }

    // Return a true or false value to check if the item is in the list
    private bool ItemInList(ObjectData objectData)
    {
        for (int i = 0; i < m_InventoryList.Count; i++)
        {
            if (m_InventoryList[i].ObjectData.Name == objectData.Name)
            {
                AddSlotAmount(m_InventoryList[i], 1); // Add one to the slot amount before returning true
                return true;
            }
        }
        return false;
    }

    // Returns the item that is currently selected
    public InventoryItem GetSelectedItem()
    {
        return m_InventoryList[m_SelectedItem];
    }
}
