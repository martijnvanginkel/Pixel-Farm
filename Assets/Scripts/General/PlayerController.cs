using System.Collections;
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

    [SerializeField] private float m_MoveSpeed;
         
    private bool m_AllowInput = true;
    public bool AllowInput
    {
        get { return m_AllowInput; }
        set { m_AllowInput = value; }
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
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);

                if (!m_FacingRight)
                {
                    FlipPlayerRight(true);
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);

                if (m_FacingRight)
                {
                    FlipPlayerRight(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_IsSlashing = true;
                GameObject standingTile = FindStandingTile();

                if (standingTile.tag == "GroundTile")
                {
                    standingTile.GetComponent<GrassTile>().Cut();
                    Debug.Log(standingTile.name);
                }
            }
        }
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

}
