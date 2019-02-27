using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBar : MonoBehaviour
{

    [SerializeField] private RectTransform m_BarSpriteTransform;

    private int m_MaxPoints = 100;
    private int m_CurrentPoints = 0;

    private float m_TransformScale = 0;

    public void GainPoints(ObjectData objectData)
    {
        m_CurrentPoints += objectData.SocialValue;
        m_TransformScale = (float)m_CurrentPoints / (float)m_MaxPoints;

        m_BarSpriteTransform.localScale = new Vector3(1, m_TransformScale, 1);
    }

}
