using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera m_Camera;
    [SerializeField] private Transform m_Player;
    [SerializeField] private float m_SmoothSpeed = 0.125f; // Between 0 and 1

    void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        MoveCamera();   
    }

    private void MoveCamera()
    {

        transform.position = Vector3.Slerp(transform.position, new Vector3(m_Player.transform.position.x, 0, -10), m_SmoothSpeed * Time.deltaTime);
    }

}


