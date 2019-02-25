using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveableObject : InteractableObject
{
    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
    //}

    [SerializeField] private GameObject m_TextBalloon;

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ReceiveItem()
    {
        InventoryItem item = Inventory.Instance.GetSelectedItem();

        Inventory.Instance.RemoveItem(item, 1);
    }


}
