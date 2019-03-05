using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataObject", menuName = "ObjectData")]
public class ObjectData : ScriptableObject
{
    public new string Name;
    public string Description;
    public string Category;

    public Sprite Icon;

    public int SocialValue;
    public int MoneyValue;

    public int DefaultStoreAmount;
    public int BuyingCost;

}
