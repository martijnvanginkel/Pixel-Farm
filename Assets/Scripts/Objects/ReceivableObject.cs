using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivableObject : InteractableObject
{
    public delegate void ReceivedItem(ObjectData objectData, string receiveType);
    public static event ReceivedItem OnReceivedItem;

    [SerializeField] protected ReceiveableData m_ObjectData;

    public virtual void ReceiveItem()
    {
        DigitalItem item = Inventory.Instance.GetSelectedItem(); // Get the currently selected item in the inventory       
        Inventory.Instance.RemoveItem(item, 1); // Remove the item from the inventory
        OnReceivedItem?.Invoke(item.ObjectData, m_ObjectData.ReceiveType); // Call an event to transfer the item into data
    }

}
