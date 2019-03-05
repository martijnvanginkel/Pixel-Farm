using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : InteractableObject
{
    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
    //}

    public delegate void ReceivedItem(ObjectData objectData, string receiveType);
    public static event ReceivedItem OnReceivedItem;

    [SerializeField] protected NpcData m_NpcData;

    [SerializeField] private GameObject m_TextBalloon;
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;


    // Update is called once per frame
    void Update()
    {
        
    }

    // Removes the received item from the inventory and triggers an event
    public virtual void ReceiveItem()
    {
        if(Inventory.Instance.InventoryList.Count == 0) // If the inventory is empty
        {
            StartCoroutine("OpenTextBalloon", m_NpcData.NothingReceivedText);
        }
        else
        {
            DigitalItem item = Inventory.Instance.GetSelectedItem(); // Get the currently selected item in the inventory       
            Inventory.Instance.RemoveItem(item, 1); // Remove the item from the inventory
            OnReceivedItem?.Invoke(item.ObjectData, m_NpcData.ReceiveType); // Call an event to transfer the item into data

            StartCoroutine("OpenTextBalloon", m_NpcData.ReceivedText);
        }
    }

    // Opens a textballoon and disables the buttonpanel
    protected virtual IEnumerator OpenTextBalloon(string text)
    {
        base.m_CanShowPanel = false;
        base.ShowButtonPanel(false);

        m_Text.text = text;
        m_TextBalloon.SetActive(true);
        yield return new WaitForSeconds(2f);
        m_TextBalloon.SetActive(false);

        base.m_CanShowPanel = true;
    }

}
