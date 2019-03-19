using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// A DigitalItem is an item that is in the inventory or in the store. It represents an item but is not physically in the scene
public class StoreSlot : DigitalItem, IPointerEnterHandler, IPointerExitHandler
{

    private bool m_MouseIsOver;

    void Awake()
    {
        m_SlotImage = GetComponent<Image>();
    }

    void SetObjectDescription()
    {
        if(ObjectData.Description != null)
        {
            m_DescriptionText.text = ObjectData.Description;
        }
    }

    public void BuyItemFromStore()
    {

        if(BackPack.Instance.CheckIfSpace(this.ObjectData))
        {
            Store.Instance.BuyItem(this);
        }
        else
        {
            Debug.Log("Cant buy backpack is full");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_MouseIsOver = true;
        StartCoroutine("WaitToShowDescription");
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_MouseIsOver = false;
        ShowDescription(false);
        Debug.Log("Mouse exit");
    }

    private IEnumerator WaitToShowDescription()
    {
        yield return new WaitForSeconds(1f);
        if (m_MouseIsOver)
        {
            ShowDescription(true);
        }
    }

    private void ShowDescription(bool isOn)
    {
        m_DescriptionBox.SetActive(isOn);
        Debug.Log("show description");
    }
}
