using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager m_Instance;
    public static DialogueManager Instance
    {
        get { return m_Instance; }
    }

    public delegate void DialogueStarted(Npc npc);
    public static event DialogueStarted OnDialogueStarted;

    public delegate void DialogueEnded();
    public static event DialogueEnded OnDialogueEnded;

    public delegate void EndOfNode(Npc npc, DialogueNode node);
    public static event EndOfNode OnEndOfNode;

    [SerializeField] private Animator m_Animator;
    [SerializeField] private TMPro.TextMeshProUGUI m_NameText;
    [SerializeField] private TMPro.TextMeshProUGUI m_DialogueText;
    [SerializeField] private GameObject m_OptionsParent;
    [SerializeField] private GameObject m_OptionPrefab;

    private List<DialogueNode> m_DialogueNodes = new List<DialogueNode>();
    private List<OptionButton> m_OptionButtons = new List<OptionButton>();
    private Queue<string> m_Sentences = new Queue<string>();

    [SerializeField] private List<Npc> m_NpcList = new List<Npc>();
    public List<Npc> NpcList
    {
        get { return m_NpcList; }
        set { m_NpcList = value; }
    }

    private List<DialogueUpdate> m_DialogueUpdates = new List<DialogueUpdate>();
    public List<DialogueUpdate> DialogueUpdates
    {
        get { return m_DialogueUpdates; }
        set { m_DialogueUpdates = value; }
    }

    private DialogueNode m_CurrentNode;
    private UpdateNode m_CurrentUpdateNode;
    private Npc m_CurrentNpc;

    private bool m_IsOpen;
    public bool IsOpen
    {
        get { return m_IsOpen; }
        set { m_IsOpen = value; }
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

    private void Start()
    {
        SetOptionButtons();
        GetAllDialogueUpdates();
    }

    private void OnEnable()
    {
        DayManager.OnEndOfDay += CloseDialogue;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfDay -= CloseDialogue;
    }

    private void GetAllDialogueUpdates()
    {
        JsonLoader jsonLoader = new JsonLoader("Dialogue", "DialogueUpdates");
        m_DialogueUpdates = jsonLoader.LoadList<DialogueUpdate>();
    }

    // Adds all the buttons in the parent to the buttonlist
    private void SetOptionButtons()
    {
        foreach (Transform child in m_OptionsParent.transform)
        {
            OptionButton button = child.GetComponent<OptionButton>();
            m_OptionButtons.Add(button);

            EnableObject(button.gameObject, false);
        }
    }

    private void Update()
    {
        if (m_IsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))
            {
                GoToNextSentence();
            }
        }
    }

    // Pop up the dialogue screen and start the conversation
    public void OpenDialogue(Npc npc, List<DialogueNode> nodes, DialogueNode startingNode)
    {
        m_CurrentNpc = npc;
        m_DialogueNodes = nodes;
        DialogueNode firstNode = startingNode;
        //m_NameText.text = m_CurrentNpc.TalkData.Name;

        OnDialogueStarted?.Invoke(npc);

        SetAnimation(true);
        StartDialogue(firstNode); 
    }

    // Pop-up the dialogue
    private void SetAnimation(bool start)
    {
        m_IsOpen = start;
        m_Animator.SetBool("IsOpen", m_IsOpen); 
    }

    // Close the dialogue screen
    private void CloseDialogue()
    {
        OnDialogueEnded?.Invoke();

        SetAnimation(false);
        SetUpNextNode();
    }

    private void SetUpNextNode()
    {
        DialogueNode followingNode = m_DialogueNodes[m_CurrentNode.FollowingNodeID - 1];
       // m_CurrentNpc.StartingNode = followingNode;

        m_CurrentNode = null;
    }

    // Start the dialogue at the first node
    public void StartDialogue(DialogueNode node)
    {
        m_CurrentNode = node;
        m_Sentences.Clear();

        // Get all the sentences from the current dialogueNode
        foreach (string sentence in m_CurrentNode.Sentences)
        {
            m_Sentences.Enqueue(sentence);
        }

        GoToNextSentence();
    }

    // Goes to the next sentence in the node
    private void GoToNextSentence()
    {
        if(m_Sentences.Count == 0)
        {
            OnEndOfNode?.Invoke(m_CurrentNpc, m_CurrentNode);

            // If theres an UpdatesStory, check what updates there are
            if (m_CurrentNode.UpdatesStory)
            {
                CheckForUpdates(m_CurrentNode, m_CurrentNpc.name);
            }

            // If theres no options after this node ends, close the dialogue otherwise show the options
            if (m_CurrentNode.DialogueOptions.Count == 0)
            {
                CloseDialogue();
            }
            else
            {
                ShowOptionButtons();
            }

            return;
        }

        string sentence = m_Sentences.Dequeue();
        StopAllCoroutines(); // Stop all coroutines in case of skipping last animation
        StartCoroutine(TypeText(sentence));
    }

    private void ShowOptionButtons()
    {
        // Turn dialogue off and buttons on
        EnableObject(m_DialogueText.gameObject, false);
        EnableObject(m_OptionsParent, true);
        EnableObject(m_NameText.gameObject, false);

        // For each option in this node
        for (int i = 0; i < m_CurrentNode.DialogueOptions.Count; i++)
        {
            EnableObject(m_OptionButtons[i].gameObject, true);
            m_OptionButtons[i].SetButtonVariables(m_CurrentNode.DialogueOptions[i]);
        }
    }

    public void NextNode(int nodeID) 
    {
        EnableObject(m_OptionsParent, false);
        EnableObject(m_DialogueText.gameObject, true);
        EnableObject(m_NameText.gameObject, true);

        foreach (OptionButton button in m_OptionButtons)
        {
            EnableObject(button.gameObject, false);
        }

        if (nodeID == -1)
        {
            CloseDialogue();
        }
        else
        {
            StartDialogue(m_DialogueNodes[nodeID - 1]);
        }
    }

    private void EnableObject(GameObject obj, bool enable)
    {
        obj.SetActive(enable);
    }

    private IEnumerator TypeText(string sentence)
    {
        m_DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            m_DialogueText.text += letter;
            yield return null; 
        }
    }

    // Finds the UpdateNode that belongs to this node and triggers the quest, cutscene or dialogue updates
    public void CheckForUpdates(DialogueNode node, string npcName)
    {
        DialogueUpdate update = GetDialogueUpdate(npcName); // Get the DialogueUpdate from this npc
        UpdateNode updateNode = GetNode(update.UpdateNodes, node); // Get the UpdateNode that matches this node
        m_CurrentUpdateNode = updateNode;

        UpdateTargets(m_CurrentUpdateNode.Targets);
    }

    // Gets the DialogueUpdate from talked to npc
    public DialogueUpdate GetDialogueUpdate(string npcName)
    {
        foreach (DialogueUpdate update in m_DialogueUpdates)
        {
            if (update.Name == npcName)
            {
                return update;
            }
        }

        return null;
    }

    public UpdateNode GetNode(UpdateNode[] updateNodes, DialogueNode node)
    {
        foreach (UpdateNode updateNode in updateNodes)
        {
            if (updateNode.ID == node.ID)
            {
                return updateNode;
            }
        }

        return null;
    }

    private void UpdateTargets(UpdateTarget[] targets)
    {
        if (targets.Length == 0)
        {
            return;
        }
        else
        {
            foreach (UpdateTarget target in targets)
            {
                foreach (Npc npc in m_NpcList)
                {
                    if (target.Name == npc.ObjectData.Name)
                    {
                        npc.StartingNode = npc.DialogueNodes[target.NextNodeID - 1];
                    }
                }
            }
        }
    }
}
