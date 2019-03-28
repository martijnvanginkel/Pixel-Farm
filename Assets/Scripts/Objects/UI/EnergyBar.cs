using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : Bar
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    [SerializeField] private TMPro.TextMeshProUGUI m_HungerValueText;
    [SerializeField] private int m_ActionDecreaseAmount;

    protected override void Start()
    {
        base.Start();
        SetEnergyText(m_CurrentValue);
    }

    private void OnEnable()
    {
        InteractableObject.OnPlayerAction += ActionDecrease;
        DayManager.OnEndOfDay += base.ResetEnergy;
    }

    private void OnDisable()
    {
        InteractableObject.OnPlayerAction -= ActionDecrease;
        DayManager.OnEndOfDay -= base.ResetEnergy;
    }

    private void ActionDecrease()
    {
        base.DecreaseValue(m_ActionDecreaseAmount);
    }

    public override void IncreaseValue(int increaseValue)
    {
        base.IncreaseValue(increaseValue);
        SetEnergyText(m_CurrentValue);
    }

    protected override void DecreaseValue(int decreaseValue)
    {
        base.DecreaseValue(decreaseValue);
        SetEnergyText(m_CurrentValue);
    }

    private void SetEnergyText(int energyValue)
    {
        m_HungerValueText.text = energyValue.ToString();
    }
}
