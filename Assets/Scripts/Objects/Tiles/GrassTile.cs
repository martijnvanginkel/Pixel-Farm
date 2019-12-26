using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : InteractableObject
{
    public delegate void PlantedSeed(ObjectData objectData);
    public static event PlantedSeed OnPlantedSeed;

    private BoxCollider2D m_BoxCollider;
    [SerializeField] private GameObject m_GrassOverlay;
    [SerializeField] private GameObject m_PlantedSeedOverlay;
    [SerializeField] private Sprite m_PlowedSprite;
    [SerializeField] private GameObject m_HealthUI;
    private Sprite m_DefaultSprite;

    //private SpriteRenderer m_SpriteRenderer;
    private GameObject m_PlantedItemPrefab;

    private Vector3 m_SpawnLocation;

    public enum State
    {
        Default,
        Planted
    }

    private State m_CurrentState;

    protected override void Start()
    {
        base.Start();

        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_DefaultSprite = m_SpriteRenderer.sprite;
        State m_CurrentState = State.Default;

        // Set the spawn location for grown plants
        float platformSize = GetComponent<Renderer>().bounds.size.y;
        m_SpawnLocation = new Vector3(transform.position.x, transform.position.y + platformSize / 2, transform.position.z);
    }

    private void OnEnable()
    {
        DayManager.OnEndOfDay += ResetTile;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfDay -= ResetTile;
    }

    private void ResetTile()
    {
        switch (m_CurrentState)
        {
            case State.Planted:
                SpawnPlant();
                break;
            default:
                break;
        }
    }

    private void SpawnPlant()
    {
        float randomFloat = Random.Range(-0.5f, 0.5f);
        m_SpawnLocation.x = m_SpawnLocation.x + randomFloat;
        Instantiate(m_PlantedItemPrefab, m_SpawnLocation, transform.rotation);

        m_PlantedSeedOverlay.SetActive(false);
        m_SpriteRenderer.sprite = m_DefaultSprite;
        m_CurrentState = State.Default;
        m_PlantedItemPrefab = null;
    }

    public void CheckIfSeedPlantable(ObjectData droppedSeed)
    {
        if (m_CurrentState == State.Default)
        {
            PlantSeed(droppedSeed);
            Inventory.Instance.RemoveSingleItem(Inventory.Instance.SelectedSlot);
        }
    }

    private void CutGrass()
    {
        m_GrassOverlay.SetActive(false);
        m_BoxCollider.enabled = true;
    }

    private void PlantSeed(ObjectData objectData)
    {
        CutGrass();
        m_PlantedSeedOverlay.SetActive(true);
        m_CurrentState = State.Planted;
        m_PlantedItemPrefab = objectData.HarvestedPlantData.Prefab;
        OnPlantedSeed?.Invoke(objectData); 
    }

    public void CheckHealth()
    {
        m_HealthUI.SetActive(true);
        ShowButtonPanel(false);
    }

    public override void ShowButtonPanel(bool showPanel)
    {
        if (IsTileOnDefault() == true)
        {
            return;
        }
        base.ShowButtonPanel(showPanel);
    }

    public bool IsTileOnDefault()
    {
        if(m_CurrentState == State.Default)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.CompareTag("Player"))
        {
            m_HealthUI.SetActive(false);
        }
    }
}
