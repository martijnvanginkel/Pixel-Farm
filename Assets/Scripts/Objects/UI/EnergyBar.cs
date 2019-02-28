using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    [SerializeField] private GameObject m_EnergyItem; // Represents a 'health' in the healthbar

    [SerializeField] private int m_TotalEnergy;
    private int m_CurrentEnergy;

    private List<EnergyItem> m_EnergyItemList = new List<EnergyItem>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnHealthItems();
    }

    private void OnEnable()
    {
        InteractableObject.OnPlayerAction += RemoveEnergy;
        DayManager.OnEndOfDay += ResetEnergy;
    }

    private void OnDisable()
    {
        InteractableObject.OnPlayerAction -= RemoveEnergy;
        DayManager.OnEndOfDay -= ResetEnergy;
    }

    private void RemoveEnergy()
    {
        if (m_CurrentEnergy > 0)
        {
            m_CurrentEnergy--;

            int healthDiff = m_TotalEnergy - m_CurrentEnergy;

            for (int i = 0; i < healthDiff; i++)
            {
                m_EnergyItemList[i].TurnOff();
            }
        }
        else
        {
            OnOutOfEnergy?.Invoke();
            Debug.Log("No Energy");
        }
    }

    private void SpawnHealthItems()
    {
        m_CurrentEnergy = m_TotalEnergy;

        for (int i = 0; i < m_TotalEnergy; i++)
        {
            GameObject energyItem = Instantiate(m_EnergyItem);
            energyItem.transform.SetParent(gameObject.transform, false); // false so it scales locally

            m_EnergyItemList.Add(energyItem.GetComponent<EnergyItem>());
        }
    }

    private void ResetEnergy()
    {
        for (int i = 0; i < m_TotalEnergy; i++)
        {
            m_EnergyItemList[i].TurnOn();
        }

        m_CurrentEnergy = m_TotalEnergy;
    }

}
