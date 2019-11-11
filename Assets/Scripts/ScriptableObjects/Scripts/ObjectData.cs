using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataObject", menuName = "ObjectData")]
public class ObjectData : ScriptableObject
{
    public new string Name;
    public string Description;

    public Sprite Icon;
    public Sprite PrefabSprite;
    public GameObject Prefab;

    public string ItemCategory;
    public string DataCategory;

    public int SocialValue;
    public int NatureValue;

    public int DefaultStoreAmount;
    public int BuyingCost;
    public int SellingCost;

    public string ReceiveType;

    public ObjectData HarvestedPlantData; // the corn that belong to this cornplant
    public int HarvestAmount;

    public int EatingValue;
}
