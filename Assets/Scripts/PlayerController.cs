using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_MoveSpeed;     
    private Rigidbody2D m_RigidBody;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Player Invisable");
    }
}
