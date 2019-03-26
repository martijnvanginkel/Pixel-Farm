using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Npc
{

    [SerializeField] private GameObject m_EggPrefab;

    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;

    private bool m_FacingRight;

    private float m_NextAnimTime = 5f;
    private float m_SetAnimTime;

    private int m_SameDirectionCounter;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SetAnimTime = m_NextAnimTime;
        m_FacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_PlayerOnObject)
        {
            CountDownTimer();
        }
    }

    private void CountDownTimer()
    {
        m_NextAnimTime -= Time.deltaTime;

        if (m_NextAnimTime < 0f)
        {
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        m_NextAnimTime = m_SetAnimTime; // Set timer back to default value

        if (RandomBool(0.33f)) // Chance of flipping the chicken in another direction
        {
            m_SameDirectionCounter = 0;
            FlipChicken();
        }
        else
        {
            m_SameDirectionCounter++;

            if (m_SameDirectionCounter == 6) // If chicken moves x amount in the same direction, flip
            {
                FlipChicken();
            }
        }

        if (RandomBool(0.1f)) // Chance of laying an egg on movement
        {
            LayEgg();
        }

        MoveChicken();
    }

    // Returns a true based on the chance given 
    private bool RandomBool(float chance)
    {
        float randomValue = Random.value;

        if(randomValue < chance)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    private void LayEgg()
    {
        Instantiate(m_EggPrefab, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }

    private void FlipChicken()
    {
        m_FacingRight = !m_FacingRight;

        m_SpriteRenderer.flipX = m_FacingRight;
    }

    private void MoveChicken()
    {
        m_Animator.SetBool("FacingRight", m_FacingRight);
        m_Animator.SetBool("ChickenMoving", true);
    }

    // Stop the animation at the last frame of the animation
    private void ChickenDoneMoving()
    {
        m_Animator.SetBool("ChickenMoving", false);
    }

}
