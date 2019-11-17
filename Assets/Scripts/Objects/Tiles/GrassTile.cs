using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : InteractableObject
{
    public delegate void PlantedSeed(ObjectData objectData);
    public static event PlantedSeed OnPlantedSeed;

    [SerializeField] private GameObject m_GrassOverlay;
    [SerializeField] private GameObject m_PlantedSeedOverlay;
    [SerializeField] private Sprite m_PlowedSprite;
    private Sprite m_DefaultSprite;

    //private SpriteRenderer m_SpriteRenderer;
    private GameObject m_PlantedItemPrefab;

    private Vector3 m_SpawnLocation;

    enum State
    {
        Default,
        Cut,
        Plowed,
        Planted,
        Recovering
    }

    private State m_CurrentState;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 

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
            case State.Cut: 
                m_GrassOverlay.SetActive(true);
                m_CurrentState = State.Default;
                break;
            case State.Plowed:
                m_SpriteRenderer.sprite = m_DefaultSprite;
                m_CurrentState = State.Cut;
                break;
            case State.Planted:
                m_PlantedSeedOverlay.SetActive(false);
                SpawnPlant();
                m_SpriteRenderer.sprite = m_DefaultSprite;
                m_CurrentState = State.Cut;
                m_PlantedItemPrefab = null;
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
    }

    public void Cut()
    {
        switch (m_CurrentState)
        {
            case State.Default:
                CutGrass(false);
                base.PlayerActionEvent();
                break;
            default:
                break;
        }
    }

    private void CutGrass(bool cutGrass)
    {
        m_GrassOverlay.SetActive(cutGrass);
        m_CurrentState = State.Cut;
    }

    public void PlantSeed(ObjectData objectData)
    {
        m_PlantedSeedOverlay.SetActive(true);
        m_CurrentState = State.Planted;
        m_PlantedItemPrefab = objectData.HarvestedPlantData.Prefab;
        OnPlantedSeed?.Invoke(objectData); 
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
}
