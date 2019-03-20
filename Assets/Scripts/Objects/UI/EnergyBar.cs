using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : Bar
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    //[SerializeField] private RectTransform m_BarTransform;

    //private int m_TotalEnergy = 100;
    //[SerializeField] private int m_CurrentEnergy = 100;

    //private float m_SetHungerDecreaseTime;
    //[SerializeField] private float m_HungerDecreaseTime;
    //[SerializeField] private int m_HungerDecreaseAmount;
    [SerializeField] private int m_ActionDecreaseAmount;

    // Start is called before the first frame update
    //void Start()
    //{
    //    m_SetHungerDecreaseTime = m_HungerDecreaseTime;
    //}

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

    //private void LateUpdate()
    //{
    //    CountDownTimer();
    //}

    //private void CountDownTimer()
    //{
    //    m_HungerDecreaseTime -= Time.deltaTime;

    //    if (m_HungerDecreaseTime < 0f)
    //    {
    //        HungerDecrease();
    //    }
    //}

    //private void HungerDecrease()
    //{
    //    m_HungerDecreaseTime = m_SetHungerDecreaseTime; // Set the time back to where it began

    //    RemoveEnergy(m_HungerDecreaseAmount);
    //}

    private void ActionDecrease()
    {
        base.DecreaseValue(m_ActionDecreaseAmount);
    }

    //private void RemoveEnergy(int decreaseValue)
    //{
    //    if(m_CurrentEnergy > 0)
    //    {
    //        m_CurrentEnergy -= decreaseValue;

    //        if(m_CurrentEnergy < 0)
    //        {
    //            Debug.Log("Out of energy");
    //        }
    //        else
    //        {
    //            m_BarTransform.localScale = new Vector3(1, (float)m_CurrentEnergy / 100f, 1);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("No Energy");
    //        OnOutOfEnergy?.Invoke();
    //    }
    //}

    //private void ResetEnergy()
    //{
    //    m_CurrentEnergy = m_TotalEnergy;
    //}
}
