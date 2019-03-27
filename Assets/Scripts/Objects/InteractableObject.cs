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

    protected SpriteRenderer m_SpriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get { return m_SpriteRenderer; }
        set { m_SpriteRenderer = value; }
    }

    private int m_SortingLayerID;
    public int SortingLayerID
    {
        get { return m_SortingLayerID; }
        set { m_SortingLayerID = value; }
    }

    [SerializeField] protected GameObject m_ButtonPanel;
    protected bool m_CanShowPanel = true;

    protected bool m_PlayerOnObject;

    [SerializeField] protected ObjectData m_ObjectData;

    protected virtual void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SortingLayerID = SortingLayer.GetLayerValueFromID(m_SpriteRenderer.sortingLayerID);
    }

    public virtual void ReceiveItem()
    {
        InventorySlot item = Inventory.Instance.SelectedSlot;
        OnReceivedItem?.Invoke(item.ObjectData, m_ObjectData.ReceiveType); // Call an event to transfer the item into data
        Inventory.Instance.RemoveItem(item);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = true;
            PlayerController.Instance.CollidingItems.Add(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = false;
            PlayerController.Instance.CollidingItems.Remove(this);
        }
    }

    public void ShowButtonPanel(bool showPanel)
    {
        m_ButtonPanel.SetActive(showPanel);
    }

    // Function to trigger OnPlayerAction event
    protected void PlayerActionEvent()
    {
        OnPlayerAction?.Invoke();
    }

}
