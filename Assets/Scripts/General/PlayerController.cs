using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalkTuple
{
    private string m_Text;
    public string Text
    {
        get { return m_Text; }
        set { m_Text = value; }
    }

    private float m_Length;
    public float Length
    {
        get { return m_Length; }
        set { m_Length = value; }
    }
}

public class PlayerController : MonoBehaviour
{
    private static PlayerController m_Instance;
    public static PlayerController Instance
    {
        get { return m_Instance; }
    }

    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private GameObject m_TextBalloon;
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;
    [SerializeField] private TMPro.TextMeshProUGUI m_CatchText;
    [SerializeField] private float m_MoveSpeed;

    private bool m_AllowInput = true;
    public bool AllowInput
    {
        get { return m_AllowInput; }
        set { m_AllowInput = value; }
    }

    [SerializeField] private bool m_HasButtonPanelOpen;
    public bool HasButtonPanelOpen
    {
        get { return m_HasButtonPanelOpen; }
        set { m_HasButtonPanelOpen = value; }
    }

    private InteractableObject m_OpenPanelObject;

    // The buttonpanel that is open above the player
    private GameObject m_OpenButtonPanel;
    public GameObject OpenButtonPanel
    {
        get { return m_OpenButtonPanel; }
        set { m_OpenButtonPanel = value; }
    }

    // List to keep track of all the items the player is currently standing on
    [SerializeField] private List<InteractableObject> m_CollidingItems = new List<InteractableObject>();
    public List<InteractableObject> CollidingItems
    {
        get { return m_CollidingItems; }
        set { m_CollidingItems = value; }
    }

    private bool m_FacingRight = true;
    private bool m_IsSlashing = false;

    private LayerMask m_TileLayer;

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

    private void OnEnable()
    {
        RandomFish.OnFishCaught += PlayerCaughtFish;
    }

    private void OnDisable()
    {
        RandomFish.OnFishCaught -= PlayerCaughtFish;
    }

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_TileLayer = LayerMask.GetMask("Tile");
    }

    private void Update()
    {
        GetInput(); // Always check for input of the player

        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(m_RigidBody.velocity.x)); // Set float for run animation
        m_Animator.SetBool("Slashing", m_IsSlashing);
    }

    // Check for user input if its allowed
    private void GetInput()
    {
        if (m_AllowInput)
        {
            if (Input.GetKey(KeyCode.D))
            {
                WalkRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                WalkLeft();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (m_HasButtonPanelOpen == false)
                {
                    //m_HasButtonPanelOpen = true;
                    OpenCollidingItem();
                }
                else
                {
                    // Take the item or whatever the firstaction may be
//                    m_OpenPanelObject.QuickAction();
                }
            }
        }
    }

    private void WalkRight()
    {
        m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);

        if (!m_FacingRight)
        {
            FlipPlayerRight(true);
        }
    }

    private void WalkLeft()
    {
        m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);

        if (m_FacingRight)
        {
            FlipPlayerRight(false);
        }
    }

    // Find the tile player is standing on and cuts the grasstile
    public void SlashTile(ObjectData objectData)
    {
        m_IsSlashing = true;
        GameObject standingTile = FindStandingTile();
        GrassTile grassTile = standingTile.GetComponent<GrassTile>();

        if (standingTile.tag == "GroundTile")
        {
            grassTile.Cut();
            grassTile.PlantSeed(objectData);
        }
    }

    // Open the buttonpanel of the object the player is currently standing on
    private void OpenCollidingItem()
    {
        InteractableObject collidingItem = null;

        if (m_CollidingItems.Count == 1) // If only one object is colliding open the first in the list
        {
            collidingItem = m_CollidingItems[0];
            collidingItem.ShowButtonPanel(true);
            m_OpenPanelObject = collidingItem;
        }
        else if (m_CollidingItems.Count > 1) // If more than 1 open the one with the highest layer
        {
            collidingItem = FindFirstItem();
            collidingItem.ShowButtonPanel(true);
            m_OpenPanelObject = collidingItem;
        }
    }

    // Return the item with the highest sortinglayer in the list of colliding items
    private InteractableObject FindFirstItem()
    {
        InteractableObject highestPriorityItem = m_CollidingItems[0];

        foreach (InteractableObject item in m_CollidingItems)
        {
            if (item.SortingLayerID > highestPriorityItem.SortingLayerID)
            {
                highestPriorityItem = item;
            }
        }

        return highestPriorityItem;
    }

    // Flips the player to the right direction and sets the bool so its only called 1 frame
    private void FlipPlayerRight(bool faceRight)
    {
        m_FacingRight = faceRight;
        m_SpriteRenderer.flipX = !faceRight;
    }

    private void PlayerDoneSlashing()
    {
        m_IsSlashing = false;
    }

    // Finds the tile the player is currently standing on
    public GameObject FindStandingTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_TileLayer);
        return hit.collider.gameObject;
    }

    public GrassTile FindStandingGrassTile()
    {
        GrassTile grassTile = FindStandingTile().GetComponent<GrassTile>();
        return grassTile;
    }

    public Transform GetPlayerPosition()
    {
        return this.transform;
    }

    public void Talk(string text, float talkLength)
    {
        PlayerTalkTuple playerTalkTuple = new PlayerTalkTuple();

        playerTalkTuple.Text = text;
        playerTalkTuple.Length = talkLength;

        StartCoroutine("OpenTextBalloonCo", playerTalkTuple);
    }

    public void Fish(bool isFishing)
    {
        m_Animator.SetBool("Fishing", isFishing);

        if (m_FacingRight == false)
        {
            FlipPlayerRight(isFishing);
        }
    }

    private void PlayerCaughtFish()
    {
        StartCoroutine("ShowCatchTextCo");
    }

    private IEnumerator ShowCatchTextCo()
    {
        m_CatchText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        m_CatchText.enabled = false;
    }

    private IEnumerator OpenTextBalloonCo(PlayerTalkTuple playerTalkTuple)
    {
        if (m_HasButtonPanelOpen)
        {
            m_OpenButtonPanel.SetActive(false);
        }

        m_Text.text = playerTalkTuple.Text;
        m_TextBalloon.SetActive(true);

        yield return new WaitForSeconds(playerTalkTuple.Length);

        m_TextBalloon.SetActive(false);

        if (m_HasButtonPanelOpen)
        {
            m_OpenButtonPanel.SetActive(true);
        }
    }

}
