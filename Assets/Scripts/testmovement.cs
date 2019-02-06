using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmovement : MonoBehaviour
{

    [SerializeField] private Vector3 m_CentrePoint;

    [SerializeField] private float m_Speed;

    [SerializeField] private Vector3 m_zAxis = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CentrePoint = transform.parent.localPosition;
        
        transform.RotateAround(m_CentrePoint, m_zAxis, -m_Speed);
    }
}
