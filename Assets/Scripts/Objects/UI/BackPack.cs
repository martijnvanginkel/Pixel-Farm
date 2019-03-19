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
    public BackPackSlot SelectedSlot
    {
        get { return m_SelectedSlot; }
        set { m_SelectedSlot = value; }
    }

    private bool m_BackPackIsFull;
    public bool BackPackIsFull
    {
        get { return m_BackPackIsFull; }
        set { m_BackPackIsFull = value; }
    }
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
        CheckForInput();
    }

    private void CheckForInput()
    {
        for (int i = 0; i < m_KeyCodes.Length; i++)
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
            DropItem(m_SelectedSlot);
        }
    }

    private void DropItem(BackPackSlot slot)
    {
        if (slot.SlotIsTaken)
        {
            GameObject tile = PlayerController.Instance.FindStandingTile();
            float height = tile.GetComponent<Renderer>().bounds.size.y;

            Instantiate(slot.ObjectData.Prefab, new Vector3(PlayerController.Instance.GetPlayerPosition().position.x, tile.transform.position.y + height / 2, tile.transform.position.z), transform.rotation);

            Debug.Log("Drop item");
            RemoveItem(m_SelectedSlot);
        }
        else
        {
            return;
        }
    }

    public void SetClickedSlotSelected(BackPackSlot slot)
    {
        m_SelectedSlot.SelectSlot(false);
        m_SelectedSlot = slot;
        m_SelectedSlot.SelectSlot(true);
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

    public void RemoveItem(BackPackSlot item)
    {
        if (item.SlotAmount > 1) // If it has more than one, decrease the amount
        {
            item.DecreaseAmount(1);
        }
        else // If it has one, remove the inventoryslot with the item in it
        {
            EmptySlot(item);
        }
    }

    private void EmptySlot(BackPackSlot slot)
    {
        if (m_BackPackIsFull)
        {
            m_BackPackIsFull = false;
        }

        slot.ResetSlot();
    }

    private BackPackSlot FindFreeSlot()
    {
        BackPackSlot freeSlot = null;
        int takenSlots = 0;

        for (int i = 0; i < m_SlotList.Count; i++)
        {

            if(m_SlotList[i].SlotIsTaken == false)
            {
                if(freeSlot == null)
                {
                    freeSlot = m_SlotList[i];
                }
            }
            else
            {
                takenSlots++;
            }
        }

        if(takenSlots == m_SlotList.Count - 1)
        {
            m_BackPackIsFull = true;
        }

        return freeSlot;

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
