using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompostBinUI : SlotsHolder
{

    [SerializeField] private Image m_BarImage;
    private RectTransform m_BarTransform;

    private float m_CurrentValue = 0;

    private void Start()
    {
        m_BarTransform = m_BarImage.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(m_SlotList.Count != 0)
        {
            GenerateCompost();
        }
    }

    private void GenerateCompost()
    {
        m_CurrentValue += Time.deltaTime;
        m_BarTransform.localScale = new Vector3(m_CurrentValue / 100f, 1f, 1f);

        if(m_CurrentValue > 100f)
        {
            m_CurrentValue = 0f;
            SpawnCompost();
        }
    }

    private void SpawnCompost()
    {

    }

    public void AddToBin(DigitalItem item)
    {
        CheckForItemCategory();

        if (CheckIfSpace(item.ObjectData))
        {
            AddItem(item.ObjectData, 1);
            Inventory.Instance.RemoveItem(item);
        }
        //m_ContainedItems.Add();
    }

    public void RemoveFromBin(DigitalItem item)
    {
        Inventory.Instance.AddItem(item.ObjectData, 1);
        RemoveItem(item);
    }

    // Check if the item has the right category to be generated into compost
    private void CheckForItemCategory()
    {

    }
}
