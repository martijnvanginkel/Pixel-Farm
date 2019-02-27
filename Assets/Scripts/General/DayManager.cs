using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    private Image m_Image;
    private Animator m_Animator;

    [SerializeField] private Image m_DarknessOverlay;
    [SerializeField] private float m_FadeSpeed;
    private Color m_DarknessColor;
    private Color m_LightColor;

    // 1f = 1 minute
    private float m_AnimationSpeed = 1f;
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
        if (m_DayTime)
        {
            m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_LightColor, Time.deltaTime * m_FadeSpeed);

            if(m_DarknessOverlay.color.a < 0.05f) // If its at the last 5% animation is done, lerping to exactly the same value takes too long
            {
                Debug.Log("its over");
            }
        }
    }

    private void Fading()
    {
        if (m_DayTime)
        {
            m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_LightColor, Time.deltaTime * m_FadeSpeed);

            if (m_DarknessOverlay.color.a < 0.05f) // If its at the last 5% animation is done, lerping to exactly the same value takes too long
            {
                m_DayTime = false;
                Debug.Log("its over");
            }
        }
    }

    private void FadeIn()
    {
        m_DayTime = true;
    }


    private void FadeOut()
    {
        m_DayTime = false;
    }

}
