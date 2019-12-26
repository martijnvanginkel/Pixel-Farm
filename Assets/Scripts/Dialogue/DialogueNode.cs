using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode
{
    private int m_ID;
    public int ID
    {
        get { return m_ID; }
        set { m_ID = value; }
    }

    private string[] m_Sentences;
    public string[] Sentences
    {
        get { return m_Sentences; }
        set { m_Sentences = value; }
    }

    private List<DialogueOption> m_DialogueOptions = new List<DialogueOption>();
    public List<DialogueOption> DialogueOptions
    {
        get { return m_DialogueOptions; }
        set { m_DialogueOptions = value; }
    }

    private int m_FollowingNodeID;
    public int FollowingNodeID
    {
        get { return m_FollowingNodeID; }
        set { m_FollowingNodeID = value; }
    }

    private bool m_UpdatesStory;
    public bool UpdatesStory
    {
        get { return m_UpdatesStory; }
        set { m_UpdatesStory = value; }
    }
}
