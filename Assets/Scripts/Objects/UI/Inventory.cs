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

    [SerializeField] private List<DigitalItem> m_InventoryList = new List<DigitalItem>();
    public List<DigitalItem> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    [SerializeField] private GameObject m_InventoryItemPrefab;
    private Image m_InventoryBackground;

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

    private void Start()
    {
        m_InventoryBackground = GetComponent<Image>();
        ToggleBackground(false);
    }

    private void Update()
    {
        CheckForScrolling();
    }

    // Function to turn the background of the inventory on or off
    private void ToggleBackground(bool isOn)
    {
        m_InventoryBackground.enabled = isOn;
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

    // Set the color bright of the item that is currently selected
    private void SetSelectedItemColor()
    {
        m_InventoryList[m_SelectedItem].SetItemSelected(); ;
        m_InventoryList[m_LastSelectedItem].SetItemUnselected();

        m_LastSelectedItem = m_SelectedItem;
    }

    // Function that gets called when an item is being picked up
    public void AddItem(ObjectData objectData, int amount)
    {
        if(m_InventoryList.Count == 0) // If inventory is empty always add the item
        {
            ToggleBackground(true);
            AddInventorySlot(objectData, amount);

            m_InventoryList[m_SelectedItem].SetItemSelected(); // Make the first item selected if the inventory was empty before
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
    public void RemoveItem(DigitalItem item, int amount)
    {
        if(item.SlotAmount > 1) // If it has more than one, decrease the amount
        {
            RemoveSlotAmount(item, amount);
        }
        else // If it has one, remove the inventoryslot with the item in it
        {
            RemoveInventorySlot(item);
        }
    }

    // Add a new inventory slot for an item that is not in the inventory
    private void AddInventorySlot(ObjectData objectData, int amount)
    {
        // Spawn new itemPrefab
        GameObject itemPrefab = Instantiate(m_InventoryItemPrefab);
        itemPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

        DigitalItem item = itemPrefab.GetComponent<DigitalItem>();
        item.ObjectData = objectData;
        item.SetAmount(amount);
        item.SetImage(objectData.Icon);

        m_InventoryList.Add(item);
    }

    // Remove an inventory slot 
    private void RemoveInventorySlot(DigitalItem item)
    {
        if(m_SelectedItem != 0 && m_SelectedItem == m_InventoryList.Count - 1) // If the last item in the list is selected and its not the only item in the list, move the selected item one back
        {
            m_SelectedItem--;
            m_LastSelectedItem--;
            m_InventoryList[m_SelectedItem].SetItemSelected();
        }

        if(m_InventoryList.Count == 1) // Turn off the background when its the last inventory item
        {
            ToggleBackground(false);
        }

        m_InventoryList.Remove(item);

        if (m_SelectedItem == 0) // If the selected item is the first item
        {
            if(m_InventoryList.Count != 0) // And its not the only item in the inventory, keep the first item selected
            {
                m_SelectedItem = 0;
                m_LastSelectedItem = 0;
                m_InventoryList[m_SelectedItem].SetItemSelected();
            }
        }
        else // If its not the first item, make the item that falls in its place selected
        {
            m_InventoryList[m_SelectedItem].SetItemSelected();
        }

        Destroy(item.gameObject);
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
    public DigitalItem GetSelectedItem()
    {
        if(m_InventoryList[m_SelectedItem] == null)
        {
            return null;
        }
        else
        {
            return m_InventoryList[m_SelectedItem];
        }

    }
}
