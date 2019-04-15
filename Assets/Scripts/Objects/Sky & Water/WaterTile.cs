using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour
{
    [SerializeField] private WaterManager m_WaterManager;
    private Animator m_Animator;

    private bool m_IsHigh;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        m_WaterManager = FindObjectOfType<WaterManager>();
       
        m_Animator.SetBool("IsHigh", m_IsHigh);
    }

    public void StartFlow()
    {
        m_Flowing = true;
        m_Animator.SetBool("Flowing", m_Flowing);
        m_IsHigh = !m_IsHigh;
    }

    private void EndOfFlow()
    {
        m_Flowing = false;
        m_Animator.SetBool("Flowing", m_Flowing);

        m_WaterManager.GoToNextTile();
    }
}
