using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuBackground : MonoBehaviour
{
    private Camera m_Camera;

    private Color m_CurrentColor;
    private Color m_TargetColor;

    private Color m_DarkColor;
    private Color m_LightColor;

    [SerializeField] private float m_FadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_DarkColor = new Color(155f / 255f, 95f / 255f, 150f / 255f, 255f / 255f);
        m_LightColor = new Color(206f / 255f, 199 / 255f, 104f / 255f, 255f / 255f);

        m_CurrentColor = m_LightColor;
        m_TargetColor = m_DarkColor;

        m_Camera = GetComponent<Camera>();
        m_Camera.backgroundColor = m_CurrentColor;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBackground();
    }

    private void ChangeBackground()
    {
        m_Camera.backgroundColor = m_CurrentColor;

        m_CurrentColor = Color.Lerp(m_CurrentColor, m_TargetColor, Time.deltaTime * m_FadeSpeed);

        if(m_CurrentColor == m_TargetColor)
        {
            if(m_CurrentColor == m_LightColor)
            {
                m_TargetColor = m_DarkColor;
            }
            else
            {
                m_TargetColor = m_LightColor;
            }
        }
    }
}
