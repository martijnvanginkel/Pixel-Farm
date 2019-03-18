using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    private static BackPack m_Instance;
    public static BackPack Instance
    {
        get { return m_Instance; }
    }

    [SerializeField] private List<BackPackSlot> m_SlotList = new List<BackPackSlot>();
    public List<BackPackSlot> SlotList
    {
        get { return m_SlotList; }
        set { m_SlotList = value; }
    }

    private BackPackSlot m_SelectedSlot;
    //private BackPackSlot m_LastSelectedSlot;

    private KeyCode[] m_KeyCodes = 
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         //KeyCode.Alpha5,
         //KeyCode.Alpha6,
         //KeyCode.Alpha7,
         //KeyCode.Alpha8,
         //KeyCode.Alpha9,
     };

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



    private void Start()
    {
        m_SelectedSlot = m_SlotList[0];
        m_SelectedSlot.SelectSlot(true);
    }

    private void Update()
    {
        for(int i = 0; i < m_KeyCodes.Length; i ++ )
        {
            if (Input.GetKeyDown(m_KeyCodes[i]))
            {
                int numberPressed = i + 1;

                m_SelectedSlot.SelectSlot(false);

                m_SelectedSlot = m_SlotList[i];
                m_SelectedSlot.SelectSlot(true);

                Debug.Log(numberPressed);
            }  
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Drop item");

            RemoveItem(m_SelectedSlot);
        }
    }

    public void AddItem(ObjectData objectData)
    {
        if (ItemInBackPack(objectData) == false)
        {
            BackPackSlot newSlot = FindFreeSlot();

            newSlot.ObjectData = objectData;
            newSlot.SlotImage.sprite = objectData.Icon;
            newSlot.SetAmount(1);
            newSlot.SlotIsTaken = true;
      
        }
    }

    public void RemoveItem(BackPackSlot slot)
    {
        if (slot.SlotAmount > 1) // If it has more than one, decrease the amount
        {
            slot.DecreaseAmount(1);
        }
        else // If it has one, remove the inventoryslot with the item in it
        {

            EmptySlot(slot);
        }
    }

    private void EmptySlot(BackPackSlot slot)
    {
        Debug.Log("emptyslot");
        slot.ObjectData = null;
        slot.SlotImage.sprite = null;
        slot.SlotIsTaken = false;
        slot.SetAmount(0);
    }

    private BackPackSlot FindFreeSlot()
    {
        foreach (BackPackSlot slot in m_SlotList)
        {
            if(slot.SlotIsTaken == false)
            {
                return slot;
            }
        }
        return null;
    }

    private bool ItemInBackPack(ObjectData objectData)
    {
        foreach (BackPackSlot slot in m_SlotList)
        {
            if(slot.ObjectData != null)
            {
                if(slot.ObjectData.Name == objectData.Name)
                {
                    slot.IncreaseAmount(1);
                    return true;
                }
            }
        }
        return false;
    }
}
