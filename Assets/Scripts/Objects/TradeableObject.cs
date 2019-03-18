using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeableObject : InteractableObject
{
    [SerializeField] protected ObjectData m_ObjectData; 

    public void TakeItem()
    {
        base.PlayerActionEvent();
        //Inventory.Instance.AddItem(m_ObjectData, 1);
        BackPack.Instance.AddItem(m_ObjectData);
        Destroy(this.gameObject);
    }
}
