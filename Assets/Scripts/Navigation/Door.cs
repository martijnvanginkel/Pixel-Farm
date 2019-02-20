using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public delegate void PlayerIsInside(bool playerInside);
    public static event PlayerIsInside OnPlayerIsInside;

    private PlayerController m_Player;
    private Animator m_Animator;
    [SerializeField] private GameObject m_LinkedDoor;

    // Start is called before the first frame update
    private void Start()
    {
        //base.Start();
        m_Animator = GetComponent<Animator>();
        m_Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("PlayerOnDoor", base.m_PlayerOnObject);
    }

    public void EnterDoor(bool outside)
    {
        if (base.m_PlayerOnObject)
        {
            OnPlayerIsInside?.Invoke(outside);
            m_Player.transform.position = m_LinkedDoor.transform.position;
        }
    }
}
