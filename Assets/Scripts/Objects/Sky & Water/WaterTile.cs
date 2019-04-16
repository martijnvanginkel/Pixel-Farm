using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour
{
    private WaterManager m_WaterManager;
    private Animator m_Animator;

    private bool m_IsHigh = false;
    public bool IsHigh
    {
        get { return m_IsHigh; }
        set { m_IsHigh = value; }
    }

    private bool m_Flowing = false;
    public bool Flowing
    {
        get { return m_Flowing; }
        set { m_Flowing = value; }
    }

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_WaterManager = GetComponentInParent<WaterManager>();
    }

    void Start()
    { 
        m_Animator.SetBool("IsHigh", m_IsHigh);
    }

    // Gets triggered from the WaterManager
    public void StartFlow()
    {
        m_Flowing = true;
        m_Animator.SetBool("Flowing", m_Flowing);
    }

    // Gets triggered at the end of the animation
    private void EndOfFlow()
    {
        m_Flowing = false;
        m_IsHigh = !m_IsHigh;
        m_Animator.SetBool("Flowing", m_Flowing);
        m_Animator.SetBool("IsHigh", m_IsHigh);

        m_WaterManager.GoToNextTile();
    }
}
