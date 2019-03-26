using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NpcData", menuName = "NpcData")]
public class NpcData : ObjectData
{
    // Text
    public string GreetingText;
    public string ReceivedText;
    public string NothingReceivedText;
    public string SurprisedText;
}
