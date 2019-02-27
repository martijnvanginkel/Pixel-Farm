using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salesman : Npc
{

    [SerializeField] private GameObject m_StoreCanvas;

    public override void ReceiveItem()
    {
        base.ReceiveItem();
    }

    public void OpenShop()
    {
        Debug.Log("Open shop UI");
        //m_StoreCanvas.SetActive(true);
    }
}
