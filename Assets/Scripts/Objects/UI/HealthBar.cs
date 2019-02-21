using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public delegate void OutOfEnergy();
    public static event OutOfEnergy OnOutOfEnergy;

    [SerializeField] private GameObject m_HealthItem; // Represents a 'health' in the healthbar

    [SerializeField] private int m_TotalHealth;
    [SerializeField] private int m_CurrentHealth;

    private List<HealthItem> m_HealthItemList = new List<HealthItem>();


    // Start is called before the first frame update
    void Start()
    {
        SpawnHealthItems();
    }

    private void OnEnable()
    {
        InteractableObject.OnPlayerAction += RemoveEnergy;
    }

    private void OnDisable()
    {
        InteractableObject.OnPlayerAction -= RemoveEnergy;
    }

    private void RemoveEnergy()
    {
        if (m_CurrentHealth > 0)
        {
            m_CurrentHealth--;

            int healthDiff = m_TotalHealth - m_CurrentHealth;

            for (int i = 0; i < healthDiff; i++)
            {
                m_HealthItemList[i].TurnOff();
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
        m_CurrentHealth = m_TotalHealth;

        for (int i = 0; i < m_TotalHealth; i++)
        {
            GameObject healthItem = Instantiate(m_HealthItem);
            healthItem.transform.SetParent(gameObject.transform, false); // false so it scales locally

            m_HealthItemList.Add(healthItem.GetComponent<HealthItem>());
        }
    }

}
