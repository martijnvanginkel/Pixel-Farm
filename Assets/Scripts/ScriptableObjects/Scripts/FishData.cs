using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FishData", menuName = "FishData")]
public class FishData : ObjectData
{
    // There's 3 different level of rareness: 1 : Common, 2: uncommon, 3 : rare
    public int CatchChance;

}
