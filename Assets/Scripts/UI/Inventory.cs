using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddItem(GameObject icon)
    {
        GameObject item = Instantiate(icon);
        item.transform.SetParent(gameObject.transform, false); // false so it scales locally

        //m_HealthItemList.Add(healthItem.GetComponent<HealthItem>());
        Debug.Log("add item");
    }

}
