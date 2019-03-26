using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ReceivableData", menuName = "ReceivableData")]
public class ReceiveableData : ObjectData
{
    // Money / Social / Fullfillment
    public string ReceiveType;

    // Character info
    public new string Name;
    public string Description;
}

