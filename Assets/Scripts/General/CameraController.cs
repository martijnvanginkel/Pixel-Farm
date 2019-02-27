using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera m_Camera;
    private Color m_BackgroundColor;

    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_SmoothSpeed = 0.125f; // Between 0 and 1

    [SerializeField] private float m_CameraOffset;

    private bool m_CameraMoving = false;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_BackgroundColor = m_Camera.backgroundColor;
    }

    void LateUpdate()
    {
        CheckPlayerPosition();

        if (m_CameraMoving)
        {
            MoveCamera();
        }
    }

    // Check if the player is outside of the camera offset
    private void CheckPlayerPosition()
    {
        if (m_Player.transform.position.x > transform.position.x + m_CameraOffset || m_Player.transform.position.x < transform.position.x - m_CameraOffset)
        {
            m_CameraMoving = true;
        }
    }

    // Move the camera to the player position and stop movement the camera reaches the position
    private void MoveCamera()
    {
        Vector3 desiredPosition = new Vector3(m_Player.transform.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, desiredPosition, m_SmoothSpeed);
        transform.position = smoothedPosition;

        if (m_Player.transform.position.x == transform.position.x)
        {
            m_CameraMoving = false;
        }
    }
}


