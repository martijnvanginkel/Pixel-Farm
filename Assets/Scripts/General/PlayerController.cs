﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if (Input.GetKeyDown(KeyCode.S))
            {
                SlashTile();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Don't open a panel if there is already one open (like a grasstile which opens by itself)
                if(m_HasButtonPanelOpen == false)
                {
                    OpenCollidingItem();
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
    private void SlashTile()
    {
        m_IsSlashing = true;
        GameObject standingTile = FindStandingTile();

        if (standingTile.tag == "GroundTile")
        {
            standingTile.GetComponent<GrassTile>().Cut();
        }
    }

    // Open the buttonpanel of the object the player is currently standing on
    private void OpenCollidingItem()
    {
        if (m_CollidingItems.Count == 1) // If only one object is colliding open the first in the list
        {
            m_CollidingItems[0].ShowButtonPanel(true);
        }
        else if (m_CollidingItems.Count > 1) // If more than 1 open the one with the highest layer
        {
            FindFirstItem().ShowButtonPanel(true);
        }
        else
        {
            Debug.Log("No object is colliding");
        }
    }

    // Return the item with the highest sortinglayer in the list of colliding items
    private InteractableObject FindFirstItem()
    {
        InteractableObject highestPriorityItem = m_CollidingItems[0];

        foreach (InteractableObject item in m_CollidingItems)
        {
            if(item.SortingLayerID < highestPriorityItem.SortingLayerID)
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
        //Debug.DrawRay(transform.position, Vector2.down, Color.green);

        return hit.collider.gameObject;
    }

    public Transform GetPlayerPosition()
    {
        return this.transform;
    }

    public void Talk(string text)
    {
        StartCoroutine("OpenTextBalloonCo", text);
    }


    private IEnumerator OpenTextBalloonCo(string text)
    {
        if (m_HasButtonPanelOpen)
        {
            m_OpenButtonPanel.SetActive(false);
        }

        m_Text.text = text;
        m_TextBalloon.SetActive(true);
        yield return new WaitForSeconds(2f);
        m_TextBalloon.SetActive(false);

        if (m_HasButtonPanelOpen)
        {
            m_OpenButtonPanel.SetActive(true);
        }
    }
}
