using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : ReceiveableObject
{

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ReceiveItem()
    {
        if(Inventory.Instance.InventoryList.Count == 0)
        {
            base.StartCoroutine("OpenTextBalloon", "You dont have anything!");
        }
        else
        {
            base.ReceiveItem();
            base.StartCoroutine("OpenTextBalloon", "Thank you!");
        }
    }

}
