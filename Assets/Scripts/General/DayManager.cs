using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public delegate void EndOfday();
    public static event EndOfday OnEndOfDay;

    private Image m_Image;
    private Animator m_Animator;

    [SerializeField] private Image m_DarknessOverlay;
    [SerializeField] private float m_FadeSpeed; // Not implemented right now
    private Color m_DarknessColor;
    private Color m_LightColor;

    // 1f = 1 minute
    private float m_AnimationSpeed = 0.2f;
    private bool m_DayTime;
    private bool m_Fading;

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("AnimationSpeed", m_AnimationSpeed);

        m_DarknessOverlay.enabled = true;

        m_DarknessColor = new Color(0f, 0f, 0f, 1f);
        m_LightColor = new Color(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        if (m_Fading)
        {
            if (m_DayTime)
            {
                FadeInOverlay();
            }
            else
            {
                FadeOutOverlay();
            }
        }
    }

    // Fades the black canvas its alpha value from 255 to 0
    private void FadeInOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_LightColor, Time.deltaTime);

        if (m_DarknessOverlay.color.a < 0.01f) // If its at the last 1% animation is done, lerping to exactly the same value takes too long
        {
            m_Fading = false;
            m_DarknessOverlay.color = m_LightColor;
            Debug.Log("its over");
        }
    }

    // Fades the black canvas its alpha value from 0 to 255
    private void FadeOutOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_DarknessColor, Time.deltaTime);

        if (m_DarknessOverlay.color.a > 0.99f) // If its at the last 1% animation is done, lerping to exactly the same value takes too long
        {
            m_Fading = false;
            m_DarknessOverlay.color = m_DarknessColor;
            Debug.Log("its over");
        }
    }

    // Get triggered by the sun animation event
    private void SunGoesUpTrigger()
    {
        m_Fading = true;
        m_DayTime = true;
    }

    // Gets trigger by the sun animation event
    private void SunGoesDownTrigger()
    {
        m_Fading = true;
        m_DayTime = false;
    }

    // Get triggered when the sun animation is at its last frame
    private void EndOfDayTrigger()
    {
        Debug.Log("End of day");
        OnEndOfDay?.Invoke();
    }

}
