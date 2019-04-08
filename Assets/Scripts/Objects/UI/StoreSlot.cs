using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// A DigitalItem is an item that is in the inventory or in the store. It represents an item but is not physically in the scene
public class StoreSlot : DigitalItem, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMPro.TextMeshProUGUI m_CostText;
    private bool m_MouseIsOver;

    void Awake()
    {
        // !! Get the first child as a fix, this is sensitive for bugs
        m_SlotImage = transform.GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        SetObjectDescription();
    }

    private void OnEnable()
    {
        DayManager.OnEndOfDay += CloseDescription;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfDay -= CloseDescription;
    }

    void SetObjectDescription()
    {
        if(ObjectData.Description != null && ObjectData.Name != null)
        {
            m_DescriptionTitle.text = ObjectData.Name;
            m_DescriptionText.text = ObjectData.Description;
            m_CostText.text = ObjectData.BuyingCost.ToString();
        }
    }

    public void BuyItemFromStore()
    {

        if(Inventory.Instance.CheckIfSpace(this.ObjectData))
        {
            Store.Instance.BuyItem(this);
        }
        else
        {
            Inventory.Instance.BackPackIsFull();
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
        yield return new WaitForSeconds(0.5f);
        if (m_MouseIsOver)
        {
            ShowDescription(true);
        }
    }

    // Wrapper function to close the description box on endofday
    private void CloseDescription()
    {
        ShowDescription(false);
    }

    private void ShowDescription(bool isOn)
    {
        m_DescriptionBox.SetActive(isOn);
        Debug.Log("show description");
    }
}
