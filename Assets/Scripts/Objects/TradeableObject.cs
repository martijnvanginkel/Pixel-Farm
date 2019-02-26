using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeableObject : InteractableObject
{
    [SerializeField] protected ObjectData m_ObjectData; 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem()
    {
        base.PlayerActionEvent();
        Inventory.Instance.AddItem(m_ObjectData, 1);
        Destroy(this.gameObject);
    }
}
