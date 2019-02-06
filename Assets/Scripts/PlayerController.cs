using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_MoveSpeed;     

    private Rigidbody2D m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_RigidBody.velocity = new Vector2(m_MoveSpeed, m_RigidBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_RigidBody.velocity = new Vector2(-m_MoveSpeed, m_RigidBody.velocity.y);
        }
    }
}
