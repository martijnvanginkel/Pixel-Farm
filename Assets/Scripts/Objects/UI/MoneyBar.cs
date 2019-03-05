using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI m_MoneyTextField;

    private int m_CurrentMoney = 0;

    private void Start()
    {
        m_MoneyTextField.text = m_CurrentMoney.ToString();
    }

    public void GainMoney(ObjectData objectData)
    {
        m_CurrentMoney += objectData.MoneyValue;

        m_MoneyTextField.text = m_CurrentMoney.ToString();
    }

    public void LoseMoney(ObjectData objectData)
    {
        m_CurrentMoney -= objectData.MoneyValue;

        m_MoneyTextField.text = m_CurrentMoney.ToString();
    }
}
