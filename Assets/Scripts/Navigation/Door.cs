using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private Animator m_Animator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("PlayerOnDoor", base.m_PlayerOnObject);
    }

    public void EnterDoor()
    {
        //OnPlayerAction?.Invoke();
    }

}
