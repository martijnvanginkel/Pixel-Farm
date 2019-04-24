using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBin : TradeableObject
{
    //private List<TradeableObject> m_ContainedItems = new List<TradeableObject>();

    [SerializeField] private List<CompostSlot> m_SlotList = new List<CompostSlot>();

    private Animator m_Animator;
    private bool m_BinOpen;

    [SerializeField] private CompostBinUI m_CompostBinUI;
    private GameObject m_CompostBinUIObject;

    protected override void Start()
    {
        base.Start();

        m_Animator = GetComponent<Animator>();

        m_CompostBinUIObject = m_CompostBinUI.gameObject;
    }

    private void Update()
    {
        if (m_BinOpen)
        {
            CheckForAnyKeysPressed();
        }
    }

    private void CheckForAnyKeysPressed()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                return;
            }
            else
            {
                CloseBin();
            }
        }
    }

    public void OpenBin()
    {
        m_BinOpen = true;
        m_Animator.SetBool("Open", m_BinOpen);
        m_CompostBinUIObject.SetActive(m_BinOpen);
        GameManager.Instance.OpenedCompostBin(this);
    }

    public void CloseBin()
    {
        m_BinOpen = false;
        m_Animator.SetBool("Open", m_BinOpen);
        m_CompostBinUIObject.SetActive(m_BinOpen);
    }

    public void FillBin()
    {

        Debug.Log("Open bin UI");
    }
    
    public void AddItemToBin(DigitalItem item)
    {
        m_CompostBinUI.AddToBin(item);
    }

    public void RemoveItemFromBin(DigitalItem item)
    {
        m_CompostBinUI.RemoveFromBin(item);
    }
}
