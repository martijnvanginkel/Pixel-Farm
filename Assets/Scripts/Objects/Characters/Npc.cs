using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : ReceivableObject
{

    [SerializeField] private GameObject m_TextBalloon;
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    [SerializeField] private NpcData m_NpcData;


    // Removes the received item from the inventory and triggers an event
    public override void ReceiveItem()
    {
        if(BackPack.Instance.SlotList.Count == 0) // If the inventory is empty
        {
            StartCoroutine("OpenTextBalloon", m_NpcData.NothingReceivedText);
        }
        else
        {

            base.ReceiveItem();

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

        // Only if the player is still colliding with the npc open the buttonpanel again
        if (m_PlayerOnObject)
        {
            base.m_CanShowPanel = true;
            base.ShowButtonPanel(true);
        }
    }

}
