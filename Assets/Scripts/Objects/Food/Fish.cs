using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Food
{
    private Animator m_Animator;
    private bool m_Diving;

    protected override void Start()
    {
        base.Start();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopDiving()
    {
        m_Diving = false;
        Destroy(this.gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WaterEdge"))
        {
            m_Diving = true;
            m_Animator.SetBool("Diving", m_Diving);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) { }
}
