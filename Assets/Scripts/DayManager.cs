using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Planet;

    [SerializeField] private Vector3 m_StartPoint;
    [SerializeField] private Vector3 m_EndPoint;
    private Vector2 m_CentrePoint;

    [SerializeField] private float m_Speed;

    private Vector3 m_zAxis = new Vector3(0, 0, 1);
    private bool m_AllowedToMove = true;
    private bool m_DayTime = true;

    void Start()
    {
        m_Planet.transform.position = m_StartPoint; // Start the planet at the start position
        m_CentrePoint = (m_StartPoint + m_EndPoint) * 0.5f; // Define m_CentrePoint to rotate around
    }

    void Update()
    {
        if (m_AllowedToMove)
        {
            RotatePlanet();
        }
    }

    // Makes the current planet rotate around the m_CentrePoint
    private void RotatePlanet()
    {
        m_Planet.transform.RotateAround(m_CentrePoint, m_zAxis, -m_Speed);

        if(m_Planet.transform.localEulerAngles.z < 180)
        {
            m_AllowedToMove = false;
        }
    }

    // TO DO : bad name
    private void ChangeDayType()
    {
        m_AllowedToMove = false;
    }

}
