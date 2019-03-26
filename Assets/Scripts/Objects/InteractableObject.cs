using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour
{

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

    public delegate void ReceivedItem(ObjectData objectData, string receiveType);
    public static event ReceivedItem OnReceivedItem;

    [SerializeField] protected GameObject m_ButtonPanel;
    protected bool m_CanShowPanel = true;

    protected bool m_PlayerOnObject;

    [SerializeField] protected ObjectData m_ObjectData;

    public virtual void ReceiveItem()
    {
        InventorySlot item = Inventory.Instance.SelectedSlot;
        OnReceivedItem?.Invoke(item.ObjectData, m_ObjectData.ReceiveType); // Call an event to transfer the item into data
        Inventory.Instance.RemoveItem(item);
    }

    // Doesnt need to be virtual right now
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = true;
            ShowButtonPanel(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = false;
            ShowButtonPanel(false);
        }
    }

    protected void ShowButtonPanel(bool showPanel)
    {
        if (m_ButtonPanel)
        {
            m_ButtonPanel.SetActive(showPanel);
        }
    }

    // bad name needs to be changed
    protected void PlayerActionEvent()
    {
        OnPlayerAction?.Invoke();
    }

}
