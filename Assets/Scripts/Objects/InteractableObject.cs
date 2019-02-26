using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour
{

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

    [SerializeField] protected GameObject m_ButtonPanel;
    protected bool m_CanShowPanel = true;

    protected bool m_PlayerOnObject;

    // Start is called before the first frame update
    private void Start()
    {
        //m_ButtonPanel.SetActive(false);
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
