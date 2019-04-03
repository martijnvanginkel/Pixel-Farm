using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBar : Bar
{
    private void OnEnable()
    {
        GrassTile.OnPlantedSeed += GainNaturePoints;
    }

    private void OnDisable()
    {
        GrassTile.OnPlantedSeed += GainNaturePoints;
    }

    public void GainNaturePoints(ObjectData objectData)
    {
        base.IncreaseValue(objectData.NatureValue);
    }
}
