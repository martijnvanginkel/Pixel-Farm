using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : InteractableObject 
{
    private List<DialogueNode> m_DialogueNodes;
    public List<DialogueNode> DialogueNodes
    {
        get { return m_DialogueNodes; }
        set { m_DialogueNodes = value; }
    }

    private DialogueNode m_StartingNode;
    public DialogueNode StartingNode
    {
        get { return m_StartingNode; }
        set { m_StartingNode = value; }
    }

    [SerializeField] private GameObject m_NameText;
    private Animator m_Animator;

    protected override void Start()
    {
        base.Start();
        AddDialogueToPlayer();

        m_Animator = GetComponent<Animator>();
    }
    public void Talk()
    {
        ShowButtonPanel(false);
        DialogueManager.Instance.OpenDialogue(this, m_DialogueNodes, m_StartingNode);
    }

    private void AddDialogueToPlayer()
    {
        JsonLoader jsonLoader = new JsonLoader("Dialogue", m_ObjectData.Name);
        m_DialogueNodes = jsonLoader.LoadList<DialogueNode>();

        m_StartingNode = m_DialogueNodes[0];
    }
}
