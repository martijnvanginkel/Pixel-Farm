using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private List<WaterTile> m_TileList = new List<WaterTile>();
    private int m_CurrentTileInt;

    void Start()
    {
        AddChildrenToList();

        m_CurrentTileInt = m_TileList.Count; // Set it equal to count cause TheNextTile immediately decreases value

        GoToNextTile();
    }

    // Puts all the WaterTile components in the list
    private void AddChildrenToList()
    {
        foreach (Transform child in this.transform)
        {
            m_TileList.Add(child.GetComponent<WaterTile>());
        }
    }

    public void GoToNextTile()
    {
        m_CurrentTileInt--;

        if(m_CurrentTileInt < 0)
        {
            m_CurrentTileInt = m_TileList.Count - 1;
        }

        m_TileList[m_CurrentTileInt].StartFlow(); 
    }
}
