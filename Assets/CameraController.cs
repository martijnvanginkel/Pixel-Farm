using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerPosition;
    private Camera m_Camera;

    private Vector3 m_ScreenPosition;
    private bool m_CameraIsMoving = false;

    [SerializeField] private float m_MoveSpeed;


    [SerializeField] private Vector3 m_LeftPosition;
    [SerializeField] private Vector3 m_CentrePosition;
    [SerializeField] private Vector3 m_RightPosition;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (!m_CameraIsMoving) 
        { 
            CheckPlayerPosition();
        }
    }

    private void CheckPlayerPosition()
    {
        m_ScreenPosition = m_Camera.WorldToScreenPoint(m_PlayerPosition.position);

        if (m_ScreenPosition.x < 0)
        {
            MoveCameraLeft();
            Debug.Log("Player left screen on left side");
        }

        if(m_ScreenPosition.x > m_Camera.pixelWidth)
        {
            Debug.Log("Player left screen on right side");
        }
    }

    private void MoveCameraLeft()
    {
        m_CameraIsMoving = true;

        Vector3 target = m_LeftPosition;

        while (transform.position != target)
        {
            float step = m_MoveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }
}
