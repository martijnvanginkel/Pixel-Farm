using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : SlotsHolder
{
    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get { return m_Instance; }
    }

    public delegate void ItemDropped(ObjectData objectData);
    public static event ItemDropped OnItemDropped;

    [SerializeField] private GameObject m_InventorySlotPrefab;
    [SerializeField] private int m_InventorySlotAmount;

    [SerializeField] private List<InventorySlot> m_InventoryList = new List<InventorySlot>();
    public List<InventorySlot> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    private InventorySlot m_SelectedSlot;
    public InventorySlot SelectedSlot
    {
        get { return m_SelectedSlot; }
        set { m_SelectedSlot = value; }
    }

    [SerializeField] private Image m_PlayerSelectedImage;

    //private bool m_InventoryIsFull;
    //public bool InventoryIsFull
    //{
    //    get { return m_InventoryIsFull; }
    //    set { m_InventoryIsFull = value; }
    //}

    private KeyCode[] m_KeyCodes = 
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8
     };

    private bool m_HoldingDropKeyDown;
    private float m_HoldingDropKeyTime = 2f;
    [SerializeField] private EnergyBar m_EnergyBar;

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
        SpawnInventorySlots();
    }

    private void SpawnInventorySlots()
    {
        for (int i = 0; i < m_InventorySlotAmount; i++)
        {
            GameObject slotPrefab = Instantiate(m_InventorySlotPrefab, this.transform);
            InventorySlot inventorySlot = slotPrefab.transform.GetChild(0).GetComponent<InventorySlot>();

            // Also add it to the slotsholder
            m_SlotList.Add(inventorySlot); 

            inventorySlot.HotKeyText.text = (i + 1).ToString();
            m_InventoryList.Add(inventorySlot);
        }

        m_SelectedSlot = m_InventoryList[0]; // Select the first item
        m_SelectedSlot.SelectSlot(true);
    }

    private void Update()
    {
        CheckForKeyInput();
    }

    private void CheckForKeyInput()
    {
        for (int i = 0; i < m_KeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(m_KeyCodes[i]))
            {
                int numberPressed = i + 1;

                SetSlotSelected(m_InventoryList[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            m_HoldingDropKeyDown = true;
        }

        // If the E button is pressed, check if the key is kept being pressed, if it is, check if the item is food, otherwise, drop the item and reset the key timer values
        if (m_HoldingDropKeyDown)
        {
            if (Input.GetKey(KeyCode.E))
            {
                CheckIfItemIsFood();
            }
            else
            {
                ResetDropKey();
                DropItem(m_SelectedSlot);
            }
        }
    }

    // Checks if item is food and otherwise drops the item on the floor
    private void CheckIfItemIsFood()
    {
        // If the selected item is null or is not of the category food, drop the item and reset timer values
        if (m_SelectedSlot.ObjectData == null || m_SelectedSlot.ObjectData.ItemCategory != "Food")
        {
            ResetDropKey();
            DropItem(m_SelectedSlot);
        }
        else
        {
            // Otherwise start the timer
            m_HoldingDropKeyTime -= Time.deltaTime; 

            // When the timer is below 0, set the key back to not being pressed, gain the health value and remove the item 
            if (m_HoldingDropKeyTime < 0f)
            {
                ResetDropKey();
                m_EnergyBar.IncreaseValue(m_SelectedSlot.ObjectData.EatingValue);
                RemoveItem(m_SelectedSlot);
            }
        }
    }

    // Resets the drop key timer values and drops the item on the floor
    private void ResetDropKey()
    {
        m_HoldingDropKeyTime = 2f;
        m_HoldingDropKeyDown = false;
    }

    private void DropItem(InventorySlot slot)
    {
        if (!slot.SlotIsTaken)
        {
            return;
        }
        else
        {
            GameObject tile = PlayerController.Instance.FindStandingTile();
            float height = tile.GetComponent<Renderer>().bounds.size.y;
            Instantiate(slot.ObjectData.Prefab, new Vector3(PlayerController.Instance.GetPlayerPosition().position.x, tile.transform.position.y + height / 2, tile.transform.position.z), transform.rotation);
            OnItemDropped?.Invoke(slot.ObjectData);
            RemoveItem(m_SelectedSlot);
        }
    }

    public void SetSlotSelected(InventorySlot slot)
    {
        m_SelectedSlot.SelectSlot(false); //  Deselect old slot
        m_SelectedSlot = slot; // Set new slot
        m_SelectedSlot.SelectSlot(true); // Select new slot
        SetPlayerIcon(slot); // Set the player icon when selecting a slot
    }

    private void SetPlayerIcon(DigitalItem slot)
    {
        if(slot.ObjectData == null)
        {
            m_PlayerSelectedImage.enabled = false;
        }
        else
        {
            if(m_PlayerSelectedImage.enabled == false)
            {
                m_PlayerSelectedImage.enabled = true;
            }
            m_PlayerSelectedImage.sprite = slot.ObjectData.Icon;
        }
    }

    //public void AddItem(ObjectData objectData, int amount)
    //{
    //    if (ItemInBackPack(objectData) == null) // If item not in the inventory fill a new slot
    //    {
    //        if(amount == 0)
    //        {
    //            FillSlot(objectData, 1);
    //        }
    //        else
    //        {
    //            FillSlot(objectData, amount);
    //        }
    //    }
    //    else
    //    {
    //        ItemInBackPack(objectData).IncreaseAmount(amount); // Increase amount if the item is already in the inventory
    //    }
    //}

    //public void RemoveItem(InventorySlot item)
    //{
    //    if (item.SlotAmount > 1) // If it has more than one, decrease the amount
    //    {
    //        item.DecreaseAmount(1);
    //    }
    //    else // If it has one, remove the inventoryslot with the item in it
    //    {
    //        // If the store is open and the last one is sold, remove the price
    //        if (Store.Instance.StoreIsOpen)
    //        {
    //            item.ShowStoreValue(false);
    //        }
    //        EmptySlot(item);
    //    }
    //}

    protected override void FillSlot(ObjectData objectData, int amount)
    {
        base.FillSlot(objectData, amount);

        // Set the player icon if the selected slot is being filled
        //if(newSlot == m_SelectedSlot)
        //{
        //    SetPlayerIcon(newSlot);
        //}


        // TO DO HOW TO SOLVE THIS?!!!!!

        //if (Store.Instance.StoreIsOpen)
        //{
        //    newSlot.ShowStoreValue(true);
        //}
    }

    //private void EmptySlot(InventorySlot slot)
    //{
    //    if (m_SlotsHolderIsFull) // Backpack is not full anymore if a slot is emptied
    //    {
    //        m_SlotsHolderIsFull = false;
    //    }

    //    slot.ResetSlot();

    //    // Reset the player icon if the selected slot is being emptied
    //    //if(slot == m_SelectedSlot)
    //    //{
    //    //    SetPlayerIcon(slot);
    //    //}
    //}

    public void ShowPrices(bool show)
    {
        foreach (InventorySlot slot in m_InventoryList)
        {
            if (slot.ObjectData != null)
            {
                slot.ShowStoreValue(show);
            }
        }
    }

    //// Finds a free slot and returns that slot
    //private InventorySlot FindFreeSlot()
    //{
    //    InventorySlot freeSlot = null;
    //    int takenSlots = 0;

    //    for (int i = 0; i < m_SlotList.Count; i++) // Loop through all slots
    //    {

    //        if(m_SlotList[i].SlotIsTaken == false) // If the slot is not taken
    //        {
    //            if(freeSlot == null) // And theres not a new slot already found
    //            {
    //                freeSlot = m_SlotList[i]; // Set this slot as the new inventory slot
    //            }
    //        }
    //        else // If the slot is taken increment the takenSlots int
    //        {
    //            takenSlots++;
    //        }
    //    }

    //    if(takenSlots == m_SlotList.Count - 1) // If all the slots are taken set the inventory to full
    //    {
    //        m_InventoryIsFull = true;
    //    }

    //    return freeSlot; // Return the slot, also if its null
    //}

    //// Loop through the slotlist and checks if theres a slot with the same name as the given object, then returns that object
    //private InventorySlot ItemInBackPack(ObjectData objectData)
    //{
    //    foreach (InventorySlot slot in m_SlotList)
    //    {
    //        if(slot.ObjectData != null)
    //        {
    //            if(slot.ObjectData.Name == objectData.Name)
    //            {
    //                return slot;
    //            }
    //        }
    //    }
    //    return null;
    //}

    //// Checks in a full inventory if the given object is already in the inventory and returns true if it is
    //public bool CheckIfSpace(ObjectData objectData)
    //{
    //    if (m_InventoryIsFull)
    //    {
    //        if (ItemInBackPack(objectData))
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    //public void ShowPrices(bool show)
    //{
    //    foreach (InventorySlot slot in m_SlotList)
    //    {
    //        if (slot.ObjectData != null)
    //        {
    //            slot.ShowStoreValue(show);
    //        }
    //    }
    //}

    //public void BackPackIsFull()
    //{
    //    StartCoroutine("BackPackFullCo");
    //}

    //private IEnumerator BackPackFullCo()
    //{
    //    foreach (InventorySlot slot in m_SlotList)
    //    {
    //        slot.LightUpSlot(true);
    //    }

    //    yield return new WaitForSeconds(0.25f);

    //    foreach (InventorySlot slot in m_SlotList)
    //    {
    //        slot.LightUpSlot(false);
    //    }
    //}
}
