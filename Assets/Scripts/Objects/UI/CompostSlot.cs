using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostSlot : DigitalItem
{
    private bool m_CompostBinIsOpen;
    public bool CompostBinIsOpen
    {
        get { return m_CompostBinIsOpen; }
        set { m_CompostBinIsOpen = value; }
    }

    public void ClickedOnCompostSlot()
    {
        if(m_ObjectData != null)
        {
            if(m_SlotAmount > 1)
            {
                Inventory.Instance.AddItem(m_ObjectData, 1);
                DecreaseAmount(1);
            }
            else if(m_SlotAmount <= 1)
            {
                Inventory.Instance.AddItem(m_ObjectData, 1);
                ResetSlot();
            }
        }
    }
}
