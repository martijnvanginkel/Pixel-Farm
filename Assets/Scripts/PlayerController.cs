using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_MoveSpeed;     
    private Rigidbody2D m_RigidBody;
    [SerializeField] private bool m_CanMove = true;

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


    // Allows the player to move when the camera is not moving
    private void DisablePlayer(bool moving)
    {
        m_CanMove = !moving;
    }

    private void GetInput()
    {
        if (m_CanMove)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
            }
        }
    }

}
