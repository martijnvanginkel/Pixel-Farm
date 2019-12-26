using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption
{
    private int m_NextNodeID;
    public int NextNodeID
    {
        get { return m_NextNodeID; }
        set { m_NextNodeID = value; }
    }

    private string m_OptionText;
    public string OptionText
    {
        get { return m_OptionText; }
        set { m_OptionText = value; }
    }
}
