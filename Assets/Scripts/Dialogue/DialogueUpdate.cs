using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUpdate
{
    private string m_Name;
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }

    private UpdateNode[] m_UpdateNode;
    public UpdateNode[] UpdateNodes
    {
        get { return m_UpdateNode; }
        set { m_UpdateNode = value; }
    }
}
