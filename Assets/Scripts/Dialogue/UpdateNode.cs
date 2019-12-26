using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateNode
{
    private int m_ID;
    public int ID
    {
        get { return m_ID; }
        set { m_ID = value; }
    }

    private UpdateTarget[] m_Targets;
    public UpdateTarget[] Targets
    {
        get { return m_Targets; }
        set { m_Targets = value; }
    }
}
