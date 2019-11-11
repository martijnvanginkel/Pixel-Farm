using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBar : Bar
{
    public delegate void SocialBarDecrease();
    public static event SocialBarDecrease OnSocialBarDecrease;

    [SerializeField] private float m_DecreaseTimer;
    [SerializeField] private float m_DecreaseAmount;
    private float m_SetDecreaseTime;

    private bool m_FirstDecrease = true;

    protected override void Start()
    {
        base.Start();

        m_SetDecreaseTime = m_DecreaseTimer;
    }

    protected override void Update()
    {
        base.Update();
        DecreaseTimer();
    }

    // Timer that always runs and decreases the value (nature and social bar)
    private void DecreaseTimer()
    {
        m_DecreaseTimer -= Time.deltaTime;

        if (m_DecreaseTimer <= 0f)
        {
            if(m_FirstDecrease == true) // If its the first time that the bar decreases, trigger the on socialbardecrease event
            {
                m_FirstDecrease = false;
                OnSocialBarDecrease?.Invoke();
            }

            m_DecreaseTimer = m_SetDecreaseTime;
            DecreaseValue(m_DecreaseAmount);
        }
    }
}
