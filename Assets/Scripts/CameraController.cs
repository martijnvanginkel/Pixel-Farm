using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public delegate void CameraMove(bool moving);
    public static event CameraMove OnCameraMove;

    [SerializeField] private Transform m_PlayerPosition;
    private Camera m_Camera;

    private Vector3 m_ScreenPosition;
    private bool m_CameraIsMoving = false;

    [SerializeField] private float m_MoveSpeed;

    [SerializeField] private Vector3 m_LeftPosition;
    [SerializeField] private Vector3 m_CentrePosition;
    [SerializeField] private Vector3 m_RightPosition;

    private Vector3 m_TargetPosition;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    void Update()
    {
        // If the camera is not moving check for playerposition, otherwise move the camera
        if (!m_CameraIsMoving) 
        { 
            CheckPlayerPosition();
        }
        else
        {
            MoveCamera();
        }
    }

    private void CheckPlayerPosition()
    {
        m_ScreenPosition = m_Camera.WorldToScreenPoint(m_PlayerPosition.position);

        if (m_ScreenPosition.x < 0)
        {
            m_TargetPosition = m_LeftPosition;
  
            OnCameraMove?.Invoke(true);
            m_CameraIsMoving = true;
            Debug.Log("Player left screen on left side");
        }
        else if(m_ScreenPosition.x > m_Camera.pixelWidth)
        {
            m_TargetPosition = m_CentrePosition;
            OnCameraMove?.Invoke(true);
            m_CameraIsMoving = true;
            Debug.Log("Player left screen on right side");
        }
    }

    // Move the camera 
    private void MoveCamera()
    {
        float step = m_MoveSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, step);    

        if(transform.position == m_TargetPosition) // If camera reaches the target position
        {
            OnCameraMove?.Invoke(false);
            m_CameraIsMoving = false;
        }
    }

}
