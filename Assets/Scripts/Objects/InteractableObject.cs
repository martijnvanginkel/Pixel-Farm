using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour
{

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

    [SerializeField] private GameObject m_ButtonPanel;
    [SerializeField] private GameObject m_IconPrefab;

    protected bool m_PlayerOnObject;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_ButtonPanel.SetActive(false);
    }

    // Doesnt need to be virtual right now
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = true;
            m_ButtonPanel.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = false;
            m_ButtonPanel.SetActive(false);
        }
    }

    public void TakeItem()
    {
        OnPlayerAction?.Invoke();
        Inventory.Instance.AddItem(m_IconPrefab);
        Destroy(this.gameObject);
    }

}
