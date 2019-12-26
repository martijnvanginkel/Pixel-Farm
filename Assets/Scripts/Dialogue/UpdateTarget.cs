using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTarget
{
    private string m_Name;
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }

    private int m_NextNodeID;
    public int NextNodeID
    {
        get { return m_NextNodeID; }
        set { m_NextNodeID = value; }
    }
}
