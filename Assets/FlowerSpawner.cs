using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    public GameObject flower;
    public Vector3 spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(flower);
        Instantiate(flower);
        Instantiate(flower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
