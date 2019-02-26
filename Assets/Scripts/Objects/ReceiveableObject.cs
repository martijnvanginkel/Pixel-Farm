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

    [SerializeField] private GameObject m_TextBalloon;
    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ReceiveItem()
    {
        InventoryItem item = Inventory.Instance.GetSelectedItem();

        Inventory.Instance.RemoveItem(item, 1);
    }

    protected virtual IEnumerator OpenTextBalloon(string text)
    {
        m_Text.text = text;
        m_TextBalloon.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_TextBalloon.SetActive(false);
    }


}
