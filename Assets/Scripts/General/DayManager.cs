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
    [SerializeField] private TMPro.TextMeshProUGUI m_SleepingText;

    [SerializeField] private float m_FadeOverlaySpeed;
    private Color m_DarkOverlayColor;
    private Color m_LightOverlayColor;

    // 1f = 1 minute, 0.2f = 5 minutes, 3f = 20 seconds
    private float m_AnimationSpeed = 6f;
    private bool m_DayTime;
    private bool m_FadingOverlay;

    private bool m_FadingCameraBackground;
    [SerializeField] private float m_FadeCameraBackgroundSpeed;

    [SerializeField] private Camera m_MainCamera;
    private Color m_MorningCameraColor;
    private Color m_EveningCameraColor;
    private Color m_DayCameraColor;
    private Color m_CurrentCameraColor;

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("AnimationSpeed", m_AnimationSpeed);

        m_DarknessOverlay.gameObject.SetActive(true);

        SetStartColors();
    }

    private void SetStartColors()
    {
        m_DarkOverlayColor = new Color(0f, 0f, 0f, 1f);
        m_LightOverlayColor = new Color(0f, 0f, 0f, 0f);
        m_MorningCameraColor = new Color(231f / 255f, 225f / 255f, 146f / 255f, 255f / 255f);
        m_EveningCameraColor = new Color(206f / 255f, 199 / 255f, 104f / 255f, 255f / 255f);
        m_DayCameraColor = new Color(155f / 255f, 95f / 255f, 150f / 255f, 255f / 255f);

        m_MainCamera.backgroundColor = m_MorningCameraColor;
        m_CurrentCameraColor = m_MorningCameraColor;
    }

    private void Update()
    {
        if (m_FadingOverlay)
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

        if (m_FadingCameraBackground)
        {
            if (m_DayTime) 
            {
                FadeInSkyColor();
            }
            else 
            {
                FadeOutSkyColor();
            }
        }
    }

    private void FadeInSkyColor()
    {
        m_MainCamera.backgroundColor = m_CurrentCameraColor;
        
        m_CurrentCameraColor = Color.Lerp(m_CurrentCameraColor, m_DayCameraColor, Time.deltaTime * m_FadeCameraBackgroundSpeed);

        if (m_CurrentCameraColor == m_DayCameraColor)
        {
            m_FadingCameraBackground = false;
        }
    }

    private void FadeOutSkyColor()
    {
        m_MainCamera.backgroundColor = m_CurrentCameraColor;

        m_CurrentCameraColor = Color.Lerp(m_CurrentCameraColor, m_EveningCameraColor, Time.deltaTime * m_FadeCameraBackgroundSpeed);
    }

    private void FadeInOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_LightOverlayColor, Time.deltaTime * m_FadeOverlaySpeed);

        if (m_DarknessOverlay.color.a < 0.01f) 
        {
            m_FadingOverlay = false;
            m_DarknessOverlay.color = m_LightOverlayColor;
        }
    }

    private void FadeOutOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_DarkOverlayColor, Time.deltaTime * m_FadeOverlaySpeed);

        if (m_DarknessOverlay.color.a > 0.99f) 
        {
            m_FadingOverlay = false;
            m_DarknessOverlay.color = m_DarkOverlayColor;
        }
    }

    private void MorningFadeTrigger()
    {
        m_FadingCameraBackground = true;
    }

    private void EveningFadeTrigger()
    {
        m_FadingCameraBackground = true;
        m_DayTime = false;
    }

    private void SunGoesUpTrigger()
    {
        m_FadingOverlay = true;
        m_DayTime = true;
    }

    private void SunGoesDownTrigger()
    {
        m_FadingOverlay = true;
    }

    private void ResetVariables()
    {
        m_CurrentCameraColor = m_MorningCameraColor;
        m_MainCamera.backgroundColor = m_CurrentCameraColor;
        m_DarknessOverlay.color = m_DarkOverlayColor;

        m_FadingCameraBackground = false;
        m_FadingOverlay = false;
    }

    // Get triggered when the sun animation is at its last frame
    private void EndOfDayTrigger()
    {
        ResetVariables();
        OnEndOfDay?.Invoke();
        StartCoroutine("WaitForNextDay");
    }

    private IEnumerator WaitForNextDay()
    {
        m_Animator.SetFloat("AnimationSpeed", 0f);
        m_SleepingText.enabled = true;
        yield return new WaitForSeconds(1f);
        m_SleepingText.enabled = false;
        m_Animator.SetFloat("AnimationSpeed", m_AnimationSpeed);
    }

}
