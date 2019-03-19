//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Inventory : MonoBehaviour
//{
//    private static Inventory m_Instance;
//    public static Inventory Instance
//    {
//        get { return m_Instance; }
//    }

//    [SerializeField] private List<StoreSlot> m_InventoryList = new List<StoreSlot>();
//    public List<StoreSlot> InventoryList
//    {
//        get { return m_InventoryList; }
//        set { m_InventoryList = value; }
//    }

//    [SerializeField] private GameObject m_InventoryItemPrefab;
//    [SerializeField] private Image m_SelectedPlayerCanvas;
//    private Image m_InventoryBackground;

//    [SerializeField] private int m_SelectedItemInt;
//    [SerializeField] private int m_LastSelectedItemInt;
//    private float m_ScrollValue; 

//    private void Awake()
//    {
//        if (m_Instance != null && m_Instance != this)
//        {
//            Destroy(this.gameObject);
//        }
//        else
//        {
//            m_Instance = this;
//        }
//    }

//    private void Start()
//    {
//        m_InventoryBackground = GetComponent<Image>();
//        //ToggleBackground(false);
//    }


//    private void Update()
//    {
//        if(Store.Instance.StoreIsOpen == false)
//        {
//            CheckForScrolling();
//        }
//    }

//    // Function to turn the background of the inventory on or off
//    private void ToggleBackground(bool isOn)
//    {
//        m_InventoryBackground.enabled = isOn;
//    }

//    //// Function that check if the player is scrolling selects current item
//    private void CheckForScrolling()
//    {
//        if(m_InventoryList.Count > 0)
//        {
//            m_ScrollValue += Input.GetAxis("Mouse ScrollWheel");
//            m_ScrollValue = Mathf.Clamp(m_ScrollValue, 0f, (float)(m_InventoryList.Count - 1));
//            m_SelectedItemInt = Mathf.RoundToInt(m_ScrollValue); // Prevents value from exceeding specified range

//            if (m_SelectedItemInt != m_LastSelectedItemInt)
//            {
//                SetSelectedItemColor();
//            }
//        }
//    }

//    // Set the color bright of the item that is currently selected
//    private void SetSelectedItemColor()
//    {
//        m_InventoryList[m_SelectedItemInt].SetItemSelected(); ;
//        m_InventoryList[m_LastSelectedItemInt].SetItemUnselected();
//        //m_SelectedPlayerCanvas.sprite = m_InventoryList[m_SelectedItemInt].ObjectData.Icon;  //turn on selected item above player head

//        m_LastSelectedItemInt = m_SelectedItemInt;
//    }

//    // Function that gets called when an item is being picked up
//    public void AddItem(ObjectData objectData, int amount)
//    {
//        if(m_InventoryList.Count == 0) // If inventory is empty always add the item
//        {
//            AddInventorySlot(objectData, amount);
//        }
//        else // If inventory is not empty 
//        {
//            if (!ItemInList(objectData)) // Add new inventory slot if its not in the list
//            {
//                AddInventorySlot(objectData, amount);
//            }
//        }
//    }

//    // Function that gets called first when an item is being received
//    public void RemoveItem(StoreSlot item, int amount)
//    {
//        if(item.SlotAmount > 1) // If it has more than one, decrease the amount
//        {
//            RemoveSlotAmount(item, amount);
//        }
//        else // If it has one, remove the inventoryslot with the item in it
//        {
//            RemoveInventorySlot(item);
//        }
//    }

//    //// Add a new inventory slot for an item that is not in the inventory
//    private void AddInventorySlot(ObjectData objectData, int amount)
//    {
//        // Spawn new itemPrefab
//        GameObject itemPrefab = Instantiate(m_InventoryItemPrefab);
//        itemPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

//        StoreSlot item = itemPrefab.GetComponent<StoreSlot>();
//        item.ObjectData = objectData;
//        item.SetAmount(amount);
//        item.SetImage(objectData.Icon);

//        if(m_InventoryList.Count == 0)
//        {
//            item.SetItemSelected();
//        }
//        else
//        {
//            item.SetItemUnselected();
//        }

//        m_InventoryList.Add(item);

//    }


//    //// Remove an inventory slot 
//    private void RemoveInventorySlot(StoreSlot item)
//    {

//        // kijk ook naar welke je op klikt, ook al is die niet geselecteerd
//        if (m_InventoryList.IndexOf(item) == m_SelectedItemInt)
//        {

//            if (m_SelectedItemInt == 0) //  als de eerste geselecteerd is
//            {
//                m_InventoryList.Remove(item);
//                Destroy(item.gameObject);

//                if (m_InventoryList.Count > 1) // en de lijst heeft meer dan 1 item
//                {
//                    m_InventoryList[m_SelectedItemInt].SetItemSelected();

//                }
//            }
//            else if (m_SelectedItemInt == m_InventoryList.Count - 1) // als de laatste geselecteerd is
//            {
//                m_InventoryList.Remove(item);
//                Destroy(item.gameObject);
//                m_SelectedItemInt--;
//                m_LastSelectedItemInt--;
//                m_InventoryList[m_SelectedItemInt].SetItemSelected();
//                return;
//            }
//            else
//            {
//                m_InventoryList.Remove(item);
//                Destroy(item.gameObject);
//                m_InventoryList[m_SelectedItemInt].SetItemSelected();
//                return;
//            }

//        }
//        else //  als je een ongeselecteerde weghaalt
//        {
//            if(m_SelectedItemInt != 0)
//            {
//                m_SelectedItemInt--;
//                m_LastSelectedItemInt--;
//            }

//            m_InventoryList.Remove(item);
//            Destroy(item.gameObject);
//        }
//    }

//    // Add slotamount to a slot that already exists
//    private void AddSlotAmount(StoreSlot item, int amount)
//    {
//        item.IncreaseAmount(amount);
//    }

//    // Decrease slotamount to a slot that already exists
//    private void RemoveSlotAmount(StoreSlot item, int amount)
//    {
//        item.DecreaseAmount(amount);
//    }

//    // Return a true or false value to check if the item is in the list
//    private bool ItemInList(ObjectData objectData)
//    {
//        for (int i = 0; i < m_InventoryList.Count; i++)
//        {
//            if (m_InventoryList[i].ObjectData.Name == objectData.Name)
//            {
//                AddSlotAmount(m_InventoryList[i], 1); // Add one to the slot amount before returning true
//                return true;
//            }
//        }
//        return false;
//    }

//    // Returns the item that is currently selected
//    public StoreSlot GetSelectedItem()
//    {
//        if(m_InventoryList.Count == 0)
//        {
//            return null; // this is not correct
//        }
//        else
//        {
//            return m_InventoryList[m_SelectedItemInt];
//        }
//    }
//}
