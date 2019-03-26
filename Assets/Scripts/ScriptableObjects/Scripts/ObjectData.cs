using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataObject", menuName = "ObjectData")]
public class ObjectData : ScriptableObject
{
    public new string Name;
    public string Description;

    // moeten nog veel meer opgedeeld worden, seeds hebben een flower en flowers weten hun eigen prefab
    public GameObject Prefab;

    public string ItemCategory;
    public string DataCategory;

    public Sprite Icon;

    public int SocialValue;

    public int DefaultStoreAmount;
    public int BuyingCost;
    public int SellingCost;

    public string ReceiveType;
}
