using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingResultSlot : DigitalItem
{
    protected override void Start()
    {
        base.Start();
    }

    public void GetItemFromTable()
    {
        if (m_ObjectData != null)
        {
            if (m_SlotAmount > 1)
            {
                Inventory.Instance.AddItem(m_ObjectData, 1);
                DecreaseAmount(1);
            }
            else
            {
                Inventory.Instance.AddItem(m_ObjectData, 1);
                EnableBackground(false);
                ResetSlot();
            }
        }
    }

    public override void FillSlot(ObjectData objectData, int amount)
    {
        base.FillSlot(objectData, amount);
        EnableBackground(true);
    }
}
