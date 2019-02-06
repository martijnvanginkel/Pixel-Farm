using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValue : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Transform target;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    void Update()
    {
        transform.RotateAround(target.position, zAxis, speed);
    }
}
