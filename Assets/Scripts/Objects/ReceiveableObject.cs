using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveableObject : InteractableObject
{
    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
    //}

    public delegate void ReceivedItem(ObjectData objectData);
    public static event ReceivedItem OnReceivedItem;

    [SerializeField] private GameObject m_TextBalloon;
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    // Update is called once per frame
    void Update()
    {
        
    }

    // Removes the received item from the inventory and triggers an event
    protected virtual void ReceiveItem()
    {
        InventoryItem item = Inventory.Instance.GetSelectedItem();       
        Inventory.Instance.RemoveItem(item, 1);

        OnReceivedItem?.Invoke(item.ObjectData);
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
