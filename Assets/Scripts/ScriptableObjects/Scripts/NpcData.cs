using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NpcData", menuName = "NpcData")]
public class NpcData : ScriptableObject
{
    // Money / Social / Fullfillment
    public string ReceiveType; 

    // Character info
    public new string Name;
    public string Description;

    // Text
    public string GreetingText;
    public string ReceivedText;
    public string NothingReceivedText;
    public string SurprisedText;

}
