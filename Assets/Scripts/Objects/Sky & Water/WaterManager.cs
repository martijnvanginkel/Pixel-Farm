using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{

    [SerializeField] private List<WaterTile> m_TileList = new List<WaterTile>();

    private int m_CurrentTileInt;
    private bool m_GoingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        AddChildrenToList();


    }

    private void AddChildrenToList()
    {
        foreach (Transform child in this.transform)
        {
            m_TileList.Add(child.GetComponent<WaterTile>());
        }

        m_CurrentTileInt = m_TileList.Count - 1;

        GoToNextTile();
    }

    public void GoToNextTile()
    {
        m_TileList[m_CurrentTileInt].StartFlow();

        if(m_GoingUp == false)
        {
            m_CurrentTileInt--;

            if(m_CurrentTileInt == 0)
            {
                m_GoingUp = true;
            }
        }
    }
}
