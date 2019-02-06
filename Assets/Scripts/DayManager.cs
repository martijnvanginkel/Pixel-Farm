using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Planet;

    [SerializeField] private Vector3 m_StartPoint;
    [SerializeField] private Vector3 m_EndPoint;
    private Vector3 m_CentrePoint;

    [SerializeField] private float m_Speed;

    private Vector3 m_zAxis = new Vector3(0, 0, 1);
    private bool m_AllowedToMove = true;

    void Start()
    {
        m_Planet.transform.localPosition = m_StartPoint; // Start the planet at the start position
        m_CentrePoint = (m_StartPoint + m_EndPoint) * 0.5f; // Define m_CentrePoint to rotate around
    }

    private void OnEnable()
    {
        CameraController.OnCameraMove += ResetPositions;
    }

    private void OnDisable()
    {
        CameraController.OnCameraMove -= ResetPositions;
    }

    private void ResetPositions(bool moving)
    {

        m_AllowedToMove = !moving;
    }


    void Update()
    {
        if (m_AllowedToMove)
        {
            RotatePlanet();
        }

    }

    private void RotatePlanet()
    {
        m_Planet.transform.RotateAround(m_CentrePoint, m_zAxis, -m_Speed); // Makes the current planet rotate around the m_CentrePoint

        if (m_Planet.transform.eulerAngles.z < 180 && m_AllowedToMove) // End of day at this z axis
        {
            m_AllowedToMove = false;
            m_Planet.transform.position = m_StartPoint;
            m_Planet.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);

            StartCoroutine("ChangeDayType");
        }
    }

    // TO DO : bad name
    private IEnumerator ChangeDayType()
    {

        yield return new WaitForSeconds(0.5f);

       
        m_AllowedToMove = true;
    }

}
