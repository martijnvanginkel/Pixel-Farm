using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TradeableObject : InteractableObject
{
    public delegate void ItemPickedUp(ObjectData objectData);
    public static event ItemPickedUp OnItemPickedUp;

    public virtual void TakeItem()
    {
        if (Inventory.Instance.CheckIfSpace(m_ObjectData))
        {
            base.PlayerActionEvent();
            Inventory.Instance.AddItem(m_ObjectData, 1);
            OnItemPickedUp?.Invoke(m_ObjectData);
            Destroy(this.gameObject);
        }
        else
        {
            Inventory.Instance.SlotsAreAllTaken();
            Debug.Log("Cant pick up backpack is full");
        }
    }

    public override void QuickAction()
    {
        TakeItem();
    }
}
