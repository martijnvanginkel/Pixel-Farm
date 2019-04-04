using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBar : Bar
{
    private void OnEnable()
    {
        GrassTile.OnPlantedSeed += PlantedSeed;
        TradeableObject.OnItemPickedUp += PickedUpNatureItem;
        Inventory.OnItemDropped += DroppedNatureItem;
    }

    private void OnDisable()
    {
        GrassTile.OnPlantedSeed -= PlantedSeed;
        TradeableObject.OnItemPickedUp -= PickedUpNatureItem;
        Inventory.OnItemDropped -= DroppedNatureItem;
    }

    public void PlantedSeed(ObjectData objectData)
    {
        base.IncreaseValue(objectData.NatureValue);
    }

    private void PickedUpNatureItem(ObjectData objectData)
    {
        switch (objectData.ItemCategory)
        {
            case "Plant":
                base.DecreaseValue(objectData.NatureValue);
                break;
            case "Animal":
                base.DecreaseValue(objectData.NatureValue);
                break;
            default:
                break;
        }
    }

    private void DroppedNatureItem(ObjectData objectData)
    {
        switch (objectData.ItemCategory)
        {
            case "Plant":
                base.IncreaseValue(objectData.NatureValue);
                break;
            case "Animal":
                base.IncreaseValue(objectData.NatureValue);
                break;
            default:
                break;
        }
    }
}
