using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableObject : MonoBehaviour
{

    public delegate void PlayerOnObject(bool onObject);
    public static event PlayerOnObject OnPlayerOnObject;

    [SerializeField] private GameObject m_ButtonPanel;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_ButtonPanel.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        OnPlayerOnObject?.Invoke(true);
        m_ButtonPanel.SetActive(true);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Entered");
        OnPlayerOnObject?.Invoke(false);
        m_ButtonPanel.SetActive(false);
    }

}
