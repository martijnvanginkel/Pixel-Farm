using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for the Inventory and CompostBinUI
public abstract class SlotsHolder : MonoBehaviour
{
    [SerializeField] protected List<DigitalItem> m_SlotList = new List<DigitalItem>();
    public List<DigitalItem> SlotList
    {
        get { return m_SlotList; }
        set { m_SlotList = value; }
    }

    protected bool m_SlotsHolderIsFull;
    public bool SlotsHolderIsFull
    {
        get { return m_SlotsHolderIsFull; }
        set { m_SlotsHolderIsFull = value; }
    }

    public void AddItem(ObjectData objectData, int amount)
    {
        if (ItemInSlotList(objectData) == null) // If item not in the inventory fill a new slot
        {
            if (amount == 0)
            {
                FillSlot(objectData, 1);
            }
            else
            {
                FillSlot(objectData, amount);
            }
        }
        else
        {
            ItemInSlotList(objectData).IncreaseAmount(amount); // Increase amount if the item is already in the inventory
        }
    }

    public void RemoveMultipleItems(DigitalItem item, int amount)
    {
        if (item.SlotAmount > amount)
        {
            item.DecreaseAmount(amount);
        }
        else
        {
            EmptySlot(item);
        }
    }

    public void RemoveSingleItem(DigitalItem item)
    {
        if (item.SlotAmount > 1) // If it has more than one, decrease the amount
        {
            item.DecreaseAmount(1);
        }
        else // If it has one, remove the inventoryslot with the item in it
        {
            EmptySlot(item);
        }
    }

    // Finds a free slot and returns that slot
    protected DigitalItem FindFreeSlot()
    {
        DigitalItem freeSlot = null;
        int takenSlots = 0;

        for (int i = 0; i < m_SlotList.Count; i++) // Loop through all slots
        {

            if (m_SlotList[i].SlotIsTaken == false) // If the slot is not taken
            {
                if (freeSlot == null) // And theres not a new slot already found
                {
                    freeSlot = m_SlotList[i]; // Set this slot as the new inventory slot
                }
            }
            else // If the slot is taken increment the takenSlots int
            {
                takenSlots++;
            }
        }

        if (takenSlots == m_SlotList.Count - 1) // If all the slots are taken set the inventory to full
        {
            m_SlotsHolderIsFull = true;
        }

        return freeSlot; // Return the slot, also if its null
    }

    // Loop through the slotlist and checks if theres a slot with the same name as the given object, then returns that object
    protected DigitalItem ItemInSlotList(ObjectData objectData)
    {
        foreach (DigitalItem slot in m_SlotList)
        {
            if (slot.ObjectData != null)
            {
                if (slot.ObjectData.Name == objectData.Name)
                {
                    return slot;
                }
            }
        }
        return null;
    }

    // Checks in a full inventory if the given object is already in the inventory and returns true if it is
    public bool CheckIfSpace(ObjectData objectData)
    {
        if (m_SlotsHolderIsFull)
        {
            if (ItemInSlotList(objectData))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    private bool AllSlotsEmpty()
    {
        foreach (DigitalItem item in m_SlotList)
        {
            if(item.ObjectData != null)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void FillSlot(ObjectData objectData, int amount)
    {
        DigitalItem newSlot = FindFreeSlot();
        newSlot.FillSlot(objectData, amount);
    }

    protected virtual void EmptySlot(DigitalItem slot)
    {
        if (m_SlotsHolderIsFull) // Backpack is not full anymore if a slot is emptied
        {
            m_SlotsHolderIsFull = false;
        }
        slot.ResetSlot();
    }

    public void SlotsAreAllTaken()
    {
        StartCoroutine("SlotsAreAllTakenCo");
    }

    private IEnumerator SlotsAreAllTakenCo()
    {
        foreach (DigitalItem slot in m_SlotList)
        {
            slot.LightUpSlot(true);
        }
        yield return new WaitForSeconds(0.25f);
        foreach (DigitalItem slot in m_SlotList)
        {
            slot.LightUpSlot(false);
        }
    }
}
