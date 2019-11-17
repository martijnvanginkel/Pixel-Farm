using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    private static DayManager m_Instance;
    public static DayManager Instance
    {
        get { return m_Instance; }
    }

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
    private float m_AnimationSpeed = 0.5f;
    private bool m_DayTime;
    private bool m_FadingOverlay;

    private bool m_FadingCameraBackground;
    [SerializeField] private float m_FadeCameraBackgroundSpeed;

    [SerializeField] private Camera m_MainCamera;
    private Color m_MorningCameraColor;
    private Color m_EveningCameraColor;
    private Color m_DayCameraColor;
    private Color m_CurrentCameraColor;

    private bool m_PlayerGoingToSleep;

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("AnimationSpeed", m_AnimationSpeed);

        m_DarknessOverlay.gameObject.SetActive(true);
        m_DayTime = true;

        SetStartColors();
    }

    private void SetStartColors()
    {
        m_DarkOverlayColor = new Color(0f, 0f, 0f, 1f);
        m_LightOverlayColor = new Color(0f, 0f, 0f, 0f);
        //m_MorningCameraColor = new Color(231f / 255f, 225f / 255f, 146f / 255f, 255f / 255f);
       //m_MorningCameraColor = new Color(155f / 255f, 95f / 255f, 150f / 255f, 255f / 255f); // temporary
        m_MorningCameraColor = new Color(115f / 255f, 94 / 255f, 150f / 255f, 255f / 255f); // lighter sky

        m_EveningCameraColor = new Color(206f / 255f, 199 / 255f, 104f / 255f, 255f / 255f);
        m_DayCameraColor = new Color(155f / 255f, 95f / 255f, 150f / 255f, 255f / 255f);

        m_MainCamera.backgroundColor = m_DayCameraColor;
        m_CurrentCameraColor = m_DayCameraColor;
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

        //if (m_FadingCameraBackground)
        //{
        //    if (m_DayTime)
        //    {
        //        FadeInSkyColor();
        //    }
        //    else
        //    {
        //        FadeOutSkyColor();
        //    }
        //}
    }

    //private void FadeInSkyColor()
    //{
    //    m_MainCamera.backgroundColor = m_CurrentCameraColor;
        
    //    m_CurrentCameraColor = Color.Lerp(m_CurrentCameraColor, m_DayCameraColor, Time.deltaTime * m_FadeCameraBackgroundSpeed);

    //    if (m_CurrentCameraColor == m_DayCameraColor)
    //    {
    //        m_FadingCameraBackground = false;
    //    }
    //}

    //private void FadeOutSkyColor()
    //{
    //    m_MainCamera.backgroundColor = m_CurrentCameraColor;

    //    m_CurrentCameraColor = Color.Lerp(m_CurrentCameraColor, m_EveningCameraColor, Time.deltaTime * m_FadeCameraBackgroundSpeed);
    //}

    private void FadeInOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_LightOverlayColor, Time.deltaTime * m_FadeOverlaySpeed * 3f);

        if (m_DarknessOverlay.color.a < 0.01f) 
        {
            m_FadingOverlay = false;
            m_DarknessOverlay.color = m_LightOverlayColor;
        }
    }

    private void FadeOutOverlay()
    {
        m_DarknessOverlay.color = Color.Lerp(m_DarknessOverlay.color, m_DarkOverlayColor, Time.deltaTime * m_FadeOverlaySpeed);

        if (m_DarknessOverlay.color.a > 0.99f && m_DayTime == false)
        {
            StartCoroutine("WaitForNextDay");
            m_DayTime = true;
        }
    }

    private void MorningTrigger()
    {
        if(m_PlayerGoingToSleep == false)
        {
            m_FadingOverlay = true;
        }
    }

    private void NightTrigger()
    {
        if (m_PlayerGoingToSleep == false)
        {
            PlayerController.Instance.AllowInput = false;
            m_DayTime = false;
            m_FadingOverlay = true;
        }
    }

    private void StopSunMovement()
    {
        m_Animator.enabled = false;
    }

    private IEnumerator WaitForNextDay()
    {
        m_FadingOverlay = false;

        m_SleepingText.enabled = true;
        yield return new WaitForSeconds(2f);

        m_Animator.Play("sun_rotation", 0, 0);
        m_FadingOverlay = true;
        

        m_PlayerGoingToSleep = false;
        m_SleepingText.enabled = false;

        OnEndOfDay?.Invoke();
        m_Animator.enabled = true;

        PlayerController.Instance.AllowInput = true;
    }

    public void PlayerGoesToBed()
    {
        PlayerController.Instance.AllowInput = false;
        m_PlayerGoingToSleep = true;
        m_DayTime = false;
        m_FadingOverlay = true;
    }

}
