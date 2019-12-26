using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : DigitalItem
{
	private CraftingTableUI m_LinkedTableUI;
	public CraftingTableUI LinkedTableUI
	{
		get { return m_LinkedTableUI; }
		set { m_LinkedTableUI = value; }
	}

    protected override void Start()
	{
		base.Start();
        m_LinkedTableUI = GetComponentInParent<CraftingTableUI>();
	}

    public void ClickedOnCraftingSlot()
    {
        if(m_ObjectData != null)
        {
			if (m_SlotAmount > 1)
			{
                Inventory.Instance.AddItem(m_ObjectData, 1);
                DecreaseAmount(1);
			}
			else
			{
				m_LinkedTableUI.RemoveItemFromTable(this);
                EnableBackground(false);
			}
            m_LinkedTableUI.RetryRecipe();
		}
    }

    public override void FillSlot(ObjectData objectData, int amount)
    {
        base.FillSlot(objectData, amount);
        EnableBackground(true);
    }
}
