using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salesman : Npc
{

    public override void ReceiveItem()
    {
        base.ReceiveItem();
    }

    public void OpenShop()
    {
        Store.Instance.OpenStorePanel(); // Open the store panel
    }
}
