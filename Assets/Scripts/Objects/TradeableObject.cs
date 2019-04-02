using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TradeableObject : InteractableObject
{
    public void TakeItem()
    {
        if (Inventory.Instance.CheckIfSpace(m_ObjectData))
        {
            base.PlayerActionEvent();
            Inventory.Instance.AddItem(m_ObjectData, 1);
            Destroy(this.gameObject);
        }
        else
        {
            Inventory.Instance.BackPackIsFull();
            Debug.Log("Cant pick up backpack is full");
        }
    }
}
