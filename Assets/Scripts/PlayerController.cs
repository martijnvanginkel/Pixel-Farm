using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

    private Rigidbody2D m_RigidBody;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private float m_MoveSpeed;     
    [SerializeField] private bool m_AllowInput = true;
    private bool m_FacingRight = true;

    [SerializeField] private bool m_ActionAllowed;


    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        CameraController.OnCameraMove += DisablePlayer;
        InteractableObject.OnPlayerOnObject += AllowPlayer;
    }
    
    private void OnDisable()
    {
        CameraController.OnCameraMove -= DisablePlayer;
        InteractableObject.OnPlayerOnObject -= AllowPlayer;
    }

    private void Update()
    {
        GetInput(); // Always check for input of the player

        m_Animator.SetFloat("MoveSpeed", Mathf.Abs(m_RigidBody.velocity.x));
    }

    private void AllowPlayer(bool onInteractableObject) // TO DO : different name
    {
        m_ActionAllowed = onInteractableObject;
        Debug.Log("Allow");
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
                    m_FacingRight = true;
                    m_SpriteRenderer.flipX = false;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
                FlipPlayer(false);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && m_ActionAllowed)
            {
                Action();
            }
        }
    }

    private void FlipPlayer(bool faceRight)
    {
        m_FacingRight = !faceRight;
        m_SpriteRenderer.flipX = !faceRight;
    }

    private void Action()
    {
        OnPlayerAction?.Invoke();
    }

}
