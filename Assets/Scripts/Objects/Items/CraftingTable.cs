using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : TradeableObject
{
    [SerializeField] private CraftingTableUI m_CraftingUI;

    protected override void Start()
    {
        base.Start();
    }

    public void OpenUI(bool open)
    {
        if (open && m_CraftingUI.gameObject.activeSelf)
        {
            return;
        }
        m_CraftingUI.gameObject.SetActive(open);
        ShowButtonPanel(false);
    }

    public override void ShowButtonPanel(bool showPanel)
    {
        if (showPanel && m_CraftingUI.gameObject.activeSelf)
        {
            return;
        }
        else
        {
            base.ShowButtonPanel(showPanel);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.CompareTag("Player"))
        {
            OpenUI(false);
        }
    }

    public override void QuickAction()
    {
        OpenUI(true);
    }
}
