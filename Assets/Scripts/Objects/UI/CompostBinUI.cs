using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostBinUI : SlotsHolder
{
    public void AddToBin(ObjectData objectData)
    {
        AddItem(objectData, 1);
        //m_ContainedItems.Add();
    }
}
