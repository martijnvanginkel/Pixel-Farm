using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBin : TradeableObject
{
    private Animator m_Animator;
    private bool m_BinOpen;

    [SerializeField] private GameObject m_TakeButton;
    [SerializeField] private GameObject m_OpenButton;
    [SerializeField] private GameObject m_FillButton;
    [SerializeField] private GameObject m_CloseButton;

    protected override void Start()
    {
        base.Start();

        m_Animator = GetComponent<Animator>();
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
        Debug.Log("Open bin UI");
    }
}
