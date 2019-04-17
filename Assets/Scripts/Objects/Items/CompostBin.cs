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

    [SerializeField] private GameObject m_TakeButton;
    [SerializeField] private GameObject m_OpenButton;
    [SerializeField] private GameObject m_FillButton;
    [SerializeField] private GameObject m_CloseButton;

    protected override void Start()
    {
        base.Start();

        m_Animator = GetComponent<Animator>();

        m_CompostBinUIObject = m_CompostBinUI.gameObject;
    }

    public void OpenBin()
    {
        m_BinOpen = true;
        m_Animator.SetBool("Open", m_BinOpen);

        m_TakeButton.SetActive(false);
        m_OpenButton.SetActive(false);
        m_CloseButton.SetActive(true);
        m_FillButton.SetActive(true);
    }

    public void CloseBin()
    {
        m_BinOpen = false;
        m_Animator.SetBool("Open", m_BinOpen);

        m_CloseButton.SetActive(false);
        m_FillButton.SetActive(false);
        m_TakeButton.SetActive(true);
        m_OpenButton.SetActive(true);
    }

    public void FillBin()
    {
        m_CompostBinUIObject.SetActive(true);
        GameManager.Instance.OpenedCompostBin(this);
        Debug.Log("Open bin UI");
    }
    
    public void AddItemToBin(ObjectData objectData)
    {
        m_CompostBinUI.AddToBin(objectData);
    }
}
