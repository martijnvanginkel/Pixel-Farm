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

    public delegate void EndOfCycle(bool endOfDay);
    public static event EndOfCycle OnEndOfCycle;

    private Image m_Image;
    private Animator m_Animator;

    [SerializeField] private Sprite m_SunSprite;
    [SerializeField] private Sprite m_MoonSprite;

    private Color m_DarkOverlayColor;
    private Color m_LightOverlayColor;

    // 1f = 1 minute, 0.2f = 5 minutes, 3f = 20 seconds
    private float m_AnimationSpeed = 1f;

    [SerializeField] private Camera m_MainCamera;
    private Color m_NightCameraColor;
    private Color m_DayCameraColor;

    private bool m_IsDaytime = true;

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

    private void OnEnable()
    {
        NightOverlay.OnDarkestPoint += SwitchDayTime;
    }

    private void OnDisable()
    {
        NightOverlay.OnDarkestPoint -= SwitchDayTime;
    }

    void Start()
    {
        m_Image = GetComponent<Image>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("AnimationSpeed", m_AnimationSpeed);

        SetStartColors();
    }

    private void SetStartColors()
    {
        m_DayCameraColor = new Color(155f / 255f, 95f / 255f, 150f / 255f, 255f / 255f);
        m_NightCameraColor = new Color(57f / 255f, 56 / 255f, 120f / 255f, 255f / 255f);
        m_MainCamera.backgroundColor = m_DayCameraColor;
    }

    public void EndOfCycleReached()
    {
        m_IsDaytime = !m_IsDaytime;
        m_Animator.enabled = false;
        OnEndOfCycle?.Invoke(m_IsDaytime);
    }

    private void SwitchDayTime(bool dayTime)
    {
        m_Animator.enabled = true;
        if (dayTime)
        {
            SetDayTime();
        }
        else
        {
            SetNightTime();
        }
    }

    private void SetDayTime()
    {
        m_IsDaytime = true;
        m_Image.sprite = m_SunSprite;
        m_MainCamera.backgroundColor = m_DayCameraColor;
        OnEndOfDay?.Invoke();
    }

    private void SetNightTime()
    {
        m_IsDaytime = false;
        m_Image.sprite = m_MoonSprite;
        m_MainCamera.backgroundColor = m_NightCameraColor;
    }

    public void PlayerGoesToBed()
    {
        //PlayerController.Instance.AllowInput = false;
        //m_PlayerGoingToSleep = true;
        //m_DayTime = false;
        //m_FadingOverlay = true;
    }

}
