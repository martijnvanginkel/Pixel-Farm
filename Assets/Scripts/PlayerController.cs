using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public delegate void PlayerAction();
    public static event PlayerAction OnPlayerAction;

    private Rigidbody2D m_RigidBody;

    [SerializeField] private float m_MoveSpeed;     
    [SerializeField] private bool m_AllowInput = true;


    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
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
        GetInput();
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
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                Action();
            }
        }
    }

    private void Action()
    {
        OnPlayerAction?.Invoke();
    }

}
