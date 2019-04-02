using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : Bar
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    [SerializeField] private int m_ActionDecreaseAmount;

    protected override void Start()
    {
        base.Start();
        //SetEnergyText(m_CurrentValue);
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

        //if(m_NewValue == 0)
        //{
        //    OnOutOfEnergy?.Invoke();
        //}
        //base.DecreaseValue(m_ActionDecreaseAmount);
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
