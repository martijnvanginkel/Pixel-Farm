using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour
{
    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

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
    public ObjectData ObjectData
    {
        get { return m_ObjectData; }
        set { m_ObjectData = value; }
    }

    protected virtual void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SortingLayerID = SortingLayer.GetLayerValueFromID(m_SpriteRenderer.sortingLayerID);
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
            ShowButtonPanel(false);
            PlayerController.Instance.CollidingItems.Remove(this);
        }
    }

    public virtual void ShowButtonPanel(bool showPanel)
    {
        m_ButtonPanel.SetActive(showPanel); // Open the buttonpanel
        PlayerController.Instance.HasButtonPanelOpen = showPanel; // Tell the player that a buttonpanel is open
        PlayerController.Instance.OpenButtonPanel = m_ButtonPanel; // Assign the buttonpanel so the player can turn it off when talking
    }

    public void QuickAction()
    {
        Debug.Log("Quick Action");
    }

    // Function to trigger OnPlayerAction event
    protected void PlayerActionEvent()
    {
        OnPlayerAction?.Invoke();
    }

}
