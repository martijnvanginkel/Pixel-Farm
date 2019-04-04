using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private RectTransform m_BarTransform;

    [SerializeField] protected float m_CurrentValue;
    public float CurrentValue
    {
        get { return m_CurrentValue; }
        set { m_CurrentValue = value; }
    }
    [SerializeField] protected TMPro.TextMeshProUGUI m_CurrentValueText;
    protected float m_NewValue; // New targeted value

    [SerializeField] private float m_Speed; // Speed to change the barscale at
    private bool m_IsChangingValue; // Checks if the barscale is currently changing

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetAmountText(m_CurrentValue);
        SetBarScale(m_CurrentValue);

        m_NewValue = m_CurrentValue;
    }

    protected virtual void Update()
    {
        if (m_IsChangingValue)
        {
            ChangeValue();
        }
    }

    private void ChangeValue() 
    {
        if (m_CurrentValue > m_NewValue)
        {
            m_CurrentValue -= Time.deltaTime * m_Speed;
        }
        else
        {
            m_CurrentValue += Time.deltaTime * m_Speed;
        }

        SetAmountText(m_CurrentValue);
        SetBarScale(m_CurrentValue);

        // If the current value reaches the new value, round up all the values 
        if (Mathf.Round(m_CurrentValue) == m_NewValue) 
        {
            m_IsChangingValue = false;
            m_CurrentValue = Mathf.Round(m_CurrentValue);
            SetBarScale(m_CurrentValue); 
            m_NewValue = m_CurrentValue; 
        }
    }

    public virtual void IncreaseValue(float increaseValue)
    {
        if (!m_IsChangingValue)
        {
            m_NewValue = m_CurrentValue + increaseValue;
        }
        else
        {
            m_NewValue += increaseValue;
        }

        if(m_NewValue > 100f)
        {
            m_NewValue = 100f;
        }

        m_IsChangingValue = true;
    }


    public virtual void DecreaseValue(float decreaseValue)
    {
        if (!m_IsChangingValue)
        {
            m_NewValue = m_CurrentValue - decreaseValue;
        }
        else
        {
            m_NewValue -= decreaseValue;
        }

        if(m_NewValue < 0f)
        {
            m_NewValue = 0f;
        }

        m_IsChangingValue = true;
    }

    protected void SetAmountText(float value)
    {
        m_CurrentValueText.text = Mathf.Round(value).ToString();
    }

    private void SetBarScale(float value)
    {
        if(m_BarTransform != null)
        {
            m_BarTransform.localScale = new Vector3(1, value / 100f, 1);
        }
    }

    protected void ResetEnergy()
    {
        m_CurrentValue = 100f;
    }
}
