using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get { return m_Instance; }
    }

    private bool m_CompostUIIsOpen;
    public bool CompostUIIsOpen
    {
        get { return m_CompostUIIsOpen; }
        set { m_CompostUIIsOpen = value; }
    }

    private CompostBin m_OpenCompostBin;
    public CompostBin OpenCompostBin
    {
        get { return m_OpenCompostBin; }
        set { m_OpenCompostBin = value; }
    }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    public void OpenedCompostBin(CompostBin openBin)
    {
        m_CompostUIIsOpen = true;
        m_OpenCompostBin = openBin;
    }

}
