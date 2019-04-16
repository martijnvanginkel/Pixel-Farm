using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : TradeableObject
{
    public void GoToBed()
    {
        DayManager.Instance.PlayerGoesToBed();
    }

}
