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

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        CameraController.OnCameraMove += DisablePlayer;
    }
    
    private void OnDisable()
    {
        CameraController.OnCameraMove -= DisablePlayer;
    }

    private void Update()
    {
        GetInput(); // Always check for input of the player

        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(m_RigidBody.velocity.x)); // Set float for run animation
    }

    // Player cant move if the OnCameraMove event is active
    private void DisablePlayer(bool moving)
    {
        m_AllowInput = !moving;
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
        }
    }

    // Flips the player to the right direction and sets the bool so its only called 1 frame
    private void FlipPlayerRight(bool faceRight)
    {
        m_FacingRight = faceRight;
        m_SpriteRenderer.flipX = !faceRight;
    }

}
