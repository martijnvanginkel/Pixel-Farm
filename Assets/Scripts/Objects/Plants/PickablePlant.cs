using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablePlant : Plant
{
    [SerializeField] private Sprite m_EmptyPlantSprite;

    [SerializeField] private GameObject m_TakeButton;
    [SerializeField] private GameObject m_PickButton;
    [SerializeField] private GameObject m_CutButton;

    private int m_HarvestAmount = 5;
    private bool m_PlantIsEmpty;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_HarvestAmount = m_ObjectData.HarvestAmount;
    }

    public void PickPlant()
    {
        if(m_PlantIsEmpty == false)
        {
            Inventory.Instance.AddItem(m_ObjectData.HarvestedPlantData, m_HarvestAmount);

            EmptyPlant();
        }
    }

    private void EmptyPlant()
    {
        m_PlantIsEmpty = true;

        m_SpriteRenderer.sprite = m_EmptyPlantSprite;

        m_TakeButton.SetActive(false);
        m_PickButton.SetActive(false);
        m_CutButton.SetActive(true);
    }

    public void CutShaft()
    {
        Destroy(this.gameObject);
    }
}
