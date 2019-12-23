using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightOverlay : MonoBehaviour
{
    private Image m_Image;
    private Animator m_Animator;

    public delegate void DarkestPoint(bool isDayTime);
    public static event DarkestPoint OnDarkestPoint;

    private bool m_IsDayTime;

    private void OnEnable()
    {
        DayManager.OnEndOfCycle += FadeOverlay;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfCycle -= FadeOverlay;
    }

    private void Start()
    {
        m_Image = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        m_Animator.enabled = false;
    }

    private void FadeOverlay(bool isDayTime)
    {
        m_IsDayTime = isDayTime;
        m_Animator.enabled = true;
        m_Animator.SetBool("IsDayTime", m_IsDayTime);
    }

    public void DarkestFadingPoint()
    {
        OnDarkestPoint?.Invoke(m_IsDayTime);
    }
}
