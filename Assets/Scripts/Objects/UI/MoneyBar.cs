using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : Bar
{
    [SerializeField] private GameObject m_MoneyDifferenceBox;
    [SerializeField] private TMPro.TextMeshProUGUI m_MoneyDifferenceIcon;
    [SerializeField] private TMPro.TextMeshProUGUI m_MoneyDifferenceText;

    private float m_ShowTime = 2f;
    private bool m_ShowMoneyDifference;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (m_ShowMoneyDifference)
        {
            ShowMoneyTimer();
        }
    }

    public void GainMoney(ObjectData objectData)
    {
        m_CurrentValue += objectData.SellingCost;
        SetAmountText(m_CurrentValue);
        m_MoneyDifferenceIcon.text = "+";
        m_MoneyDifferenceText.text = objectData.SellingCost.ToString();

        ShowDifferenceBox(true);
    }

    public void LoseMoney(ObjectData objectData)
    {
        m_CurrentValue -= objectData.BuyingCost;
        SetAmountText(m_CurrentValue);
        m_MoneyDifferenceIcon.text = "-";
        m_MoneyDifferenceText.text = objectData.BuyingCost.ToString();

        ShowDifferenceBox(true);
    }

    private void ShowDifferenceBox(bool show)
    {
        m_MoneyDifferenceBox.SetActive(show);
        m_ShowMoneyDifference = show;
    }


    private void ShowMoneyTimer()
    {
        m_ShowTime -= Time.deltaTime;

        if(m_ShowTime <= 0f)
        {
            ShowDifferenceBox(false);
            m_ShowTime = 2f;
        }
    }

}
