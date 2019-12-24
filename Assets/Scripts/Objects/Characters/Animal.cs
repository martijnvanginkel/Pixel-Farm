using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : TradeableObject
{
    private Animator m_Animator;

    private bool m_FacingRight;

    [SerializeField] private float m_NextAnimTime;
    private float m_SetAnimTime;
    private int m_SameDirectionCounter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_Animator = GetComponent<Animator>();
        m_SetAnimTime = m_NextAnimTime;
        m_FacingRight = false;

        SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        if (RandomBool(0.5f))
        {
            FlipAnimal();
        }

        MoveAnimal();
    }

    // Update is called once per frame
    private void Update()
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

    protected virtual void ResetTimer()
    {
        m_NextAnimTime = m_SetAnimTime; // Set timer back to default value

        if (RandomBool(0.33f)) // Chance of flipping the chicken in another direction
        {
            m_SameDirectionCounter = 0;
            FlipAnimal();
        }
        else
        {
            m_SameDirectionCounter++;

            if (m_SameDirectionCounter == 6) // If chicken moves x amount in the same direction, flip
            {
                FlipAnimal();
            }
        }
    }

    protected bool RandomBool(float chance)
    {
        float randomValue = Random.value;

        if (randomValue < chance)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    private void FlipAnimal()
    {
        m_FacingRight = !m_FacingRight;

        m_SpriteRenderer.flipX = m_FacingRight;
    }

    protected void MoveAnimal()
    {
        m_Animator.SetBool("FacingRight", m_FacingRight);
        m_Animator.SetBool("AnimalMoving", true);
    }

    // Stop the animation at the last frame of the animation
    private void AnimalDoneMoving()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        m_Animator.SetBool("AnimalMoving", false);
    }
}
