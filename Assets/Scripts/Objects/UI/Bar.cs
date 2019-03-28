using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private RectTransform m_BarTransform;

    private int m_TotalValue = 100;
    [SerializeField] private int m_CurrentValue = 100;

    private float m_SetDecreaseTime;
    [SerializeField] private float m_DecreaseTime;
    [SerializeField] private int m_DecreaseAmount;

    //[SerializeField] private int m_ActionDecreaseAmount;

    // Start is called before the first frame update
    void Start()
    {
        m_SetDecreaseTime = m_DecreaseTime;
    }

    private void LateUpdate()
    {
        CountDownTimer();
    }

    private void CountDownTimer()
    {
        m_DecreaseTime -= Time.deltaTime;

        if (m_DecreaseTime < 0f)
        {
            TimeDecrease();
        }
    }

    private void TimeDecrease()
    {
        m_DecreaseTime = m_SetDecreaseTime; // Set the time back to where it began

        DecreaseValue(m_DecreaseAmount);
    }

    public void IncreaseValue(int increaseValue)
    {
        if(m_CurrentValue < 100)
        {
            m_CurrentValue += increaseValue;

            if (m_CurrentValue > 100)
            {
                Debug.Log("Full");
            }
            else
            {
                m_BarTransform.localScale = new Vector3(1, (float)m_CurrentValue / 100f, 1);
            }
        }
        else
        {
            Debug.Log("Full");
        }
    }

    protected void DecreaseValue(int decreaseValue)
    {
        if (m_CurrentValue > 0)
        {
            m_CurrentValue -= decreaseValue;

            if (m_CurrentValue < 0)
            {
                Debug.Log("Empty");
            }
            else
            {
                m_BarTransform.localScale = new Vector3(1, (float)m_CurrentValue / 100f, 1);
            }
        }
    }

    protected void ResetEnergy()
    {
        m_CurrentValue = m_TotalValue;
    }
}
