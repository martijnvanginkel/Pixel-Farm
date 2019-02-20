using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static Inventory m_Instance;
    public static Inventory Instance
    {
        get { return m_Instance; }
    }

    private Dictionary<TradeableObject, int> m_InventoryList = new Dictionary<TradeableObject, int>();
    public Dictionary<TradeableObject, int> InventoryList
    {
        get { return m_InventoryList; }
        set { m_InventoryList = value; }
    }

    [SerializeField] private GameObject m_InventorySlotPrefab;



    // needs to be changed, bad name 
    public int ScrollInt;

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

    private void Update()
    {
        CheckForScrolling();
    }

    private void CheckForScrolling()
    {

    
        ScrollInt += Mathf.RoundToInt(Input.GetAxis("Mouse ScrollWheel"));
        ScrollInt = Mathf.Clamp(ScrollInt, 0, 10);//prevents value from exceeding specified range
        Debug.Log(ScrollInt);


    }

    public void AddItem(TradeableObject tradeableObject, int amount)
    {
        m_InventoryList.Add(tradeableObject, amount);

        GameObject slotPrefab = Instantiate(m_InventorySlotPrefab);
        slotPrefab.transform.SetParent(gameObject.transform, false); // false so it scales locally

        InventorySlot inventorySlot = slotPrefab.GetComponent<InventorySlot>();
        inventorySlot.SetImage(tradeableObject.Icon);
        inventorySlot.SetAmount(amount);


        //inventorySlot.SetImage(tradeableObject.Icon);
        //inventorySlot.transform.SetParent(gameObject.transform, false); // false so it scales locally


        foreach (var item in m_InventoryList)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
        //GameObject item = Instantiate(icon);
        //item.transform.SetParent(gameObject.transform, false); // false so it scales locally

        //m_InventoryList.Add(item.GetComponent<InteractableObject>(), 0);

        //m_HealthItemList.Add(healthItem.GetComponent<HealthItem>());
        Debug.Log("add item");
    }

}
