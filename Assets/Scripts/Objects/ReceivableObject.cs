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
        InventorySlot item = Inventory.Instance.SelectedSlot;
        OnReceivedItem?.Invoke(item.ObjectData, m_ObjectData.ReceiveType); // Call an event to transfer the item into data
        Inventory.Instance.RemoveItem(item);
    }
}
