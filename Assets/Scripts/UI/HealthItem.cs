﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private Color m_OnColor;
    [SerializeField] private Color m_OffColor;
    private Image m_Image;

    private void Start()
    {
        m_Image = GetComponent<Image>();
    }

    public void TurnOff()
    {
        m_Image.color = m_OffColor;
    }
}
