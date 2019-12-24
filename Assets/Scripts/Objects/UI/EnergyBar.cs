using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : Bar
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    [SerializeField] private int m_ActionDecreaseAmount;
    private float m_DecreaseStartTime = 30f;
    private float m_DecreaseTimer;

    protected override void Start()
    {
        base.Start();
        m_DecreaseTimer = m_DecreaseStartTime;
    }

    private void OnEnable()
    {
        Inventory.OnFoodEaten += FoodEaten;
        DayManager.OnEndOfDay += base.ResetEnergy;
    }

    private void OnDisable()
    {
        Inventory.OnFoodEaten -= FoodEaten;
        DayManager.OnEndOfDay -= base.ResetEnergy;
    }

    protected override void Update()
    {
        base.Update();
        DecreaseEnergyTimer();
    }

    private void DecreaseEnergyTimer()
    {
        m_DecreaseTimer -= Time.deltaTime;
        if (m_DecreaseTimer < 0f)
        {
            ActionDecrease();
            m_DecreaseTimer = m_DecreaseStartTime;
        }
    }

    private void FoodEaten(ObjectData eatenObject)
    {
        IncreaseValue(eatenObject.EatingValue);
    }

    private void ActionDecrease()
    {
        base.DecreaseValue(m_ActionDecreaseAmount);
    }

    public override void IncreaseValue(float increaseValue)
    {
        base.IncreaseValue(increaseValue);
    }

    public override void DecreaseValue(float decreaseValue)
    {
        base.DecreaseValue(decreaseValue);
    }
}
