using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    enum State
    {
        Default,
        Cut,
        Plowed,
        Planted
    }

    private State m_CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        State m_CurrentState = State.Grown;
    }

    public void Cut()
    {
        switch (m_CurrentState)
        {
            case State.Default:
                print("-");
                break;
            case State.Cut:
                print("-");
                break;
            case State.Plowed:
                print("-");
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
