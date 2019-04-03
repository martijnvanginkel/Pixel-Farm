using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialBar : Bar
{
    private void OnEnable()
    {
        InteractableObject.OnReceivedItem += GainSocialPoints;
    }

    private void OnDisable()
    {
        InteractableObject.OnReceivedItem -= GainSocialPoints;
    }

    public void GainSocialPoints(ObjectData objectData)
    {
        base.IncreaseValue(objectData.SocialValue);
    }
}
