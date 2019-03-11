using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private float m_MoveSpeed;     
    [SerializeField] private bool m_AllowInput = true;
    private bool m_FacingRight = true;
    private bool m_IsSlashing = false;

    private LayerMask m_TileLayer;



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

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_TileLayer);
                Debug.DrawRay(transform.position, Vector2.down, Color.green);

                if (hit.collider.tag == "GroundTile")
                {
                    hit.collider.gameObject.GetComponent<GroundTile>().Cut();
                    Debug.Log(hit.collider.name);
                    
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

}
