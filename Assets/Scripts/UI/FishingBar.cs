using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingBar : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField] private FishingSpot m_FishingSpot;

    private bool m_UIOpen;
    private bool m_InCatchRange;

    [SerializeField] private RectTransform m_CollidingPoint;
    private BoxCollider2D m_CollidingPointCollider;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_CollidingPointCollider = m_CollidingPoint.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (m_UIOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_InCatchRange) // If the mouse gets clicked inside the catchrange catch a fish, otherwish only stop the game
                {
                    m_FishingSpot.CatchFish();
                }

                StopFishGame();
            }
        }
    }

    // Set a random width, position and speed for the minigame
    private void RandomGameVariables()
    {
        float randomWidth = Random.Range(8f, 60f);
        float randomPosition = Random.Range(50f, 160f);
        float randomSpeed = Random.Range(1.5f, 4f);

        m_CollidingPoint.sizeDelta = new Vector2(randomWidth, m_CollidingPoint.sizeDelta.y);
        m_CollidingPoint.anchoredPosition = new Vector2(randomPosition, m_CollidingPoint.anchoredPosition.y);

        // Also scale the collider with the new width
        m_CollidingPointCollider.size = new Vector2(randomWidth, m_CollidingPointCollider.size.y);

        m_Animator.SetFloat("Speed", randomSpeed);
    }

    private void EnterCatchRange(bool entered)
    {
        m_InCatchRange = entered;
    }

    public void OpenFishGame()
    {
        m_UIOpen = true;
        m_Animator.SetBool("Fishing", m_UIOpen);

        RandomGameVariables();
    }

    private void StopFishGame()
    {
        m_UIOpen = false;
        m_Animator.SetBool("Fishing", m_UIOpen);

        m_FishingSpot.StopFishing();
        EnterCatchRange(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("UI"))
        {
            EnterCatchRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("UI"))
        {
            EnterCatchRange(false);
        }
    }

}
