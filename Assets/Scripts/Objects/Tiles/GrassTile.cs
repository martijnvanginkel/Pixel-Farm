using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : InteractableObject
{
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
                m_PlantedSeedOverlay.SetActive(false); // Turn seedsprite off
                Instantiate(m_PlantedItemPrefab, m_SpawnLocation, transform.rotation); // Spawn flower on top of the tile
                m_CurrentState = State.Recovering; // Set state to recovery
                m_PlantedItemPrefab = null; // Set prefab back to null after spawning
                break;
            case State.Recovering:
                m_SpriteRenderer.sprite = m_DefaultSprite;
                //m_GrassOverlay.SetActive(true);
                m_CurrentState = State.Cut;
                break;
            default:
                break;
        }
    }

    public override void ReceiveItem()
    {
        InventorySlot item = Inventory.Instance.SelectedSlot; // Get the currently selected item

        switch (item.ObjectData.ItemCategory)
        {
            case "Seeds": // Receive the item if its a seed and plant the seed
                PlantSeed(item.ObjectData);
                base.ReceiveItem();
                break;
            default:
                print("-");
                break;
        }
    }

    // Go to function when the downarrow is being clicked by the player, checks for current state of the groundtile
    public void Cut()
    {
        switch (m_CurrentState)
        {
            case State.Default:
                CutGrass(false);
                base.PlayerActionEvent();
                break;
            case State.Cut:
                PlowTile();
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

    private void PlowTile()
    {
        m_SpriteRenderer.sprite = m_PlowedSprite;
        m_CurrentState = State.Plowed;
        ShowButtonPanel();
    }

    private void PlantSeed(ObjectData objectData)
    {
        m_PlantedSeedOverlay.SetActive(true); // Turn seedoverlay sprite on

        m_CurrentState = State.Planted;
        m_PlantedItemPrefab = objectData.Prefab;

        ShowButtonPanel(false); // Turn buttonPanel off
        base.PlayerActionEvent(); 
    }

    // Button panel is being showed when the inventory is selecting a seedpackage
    private void ShowButtonPanel()
    {
        if (Inventory.Instance.SelectedSlot.SlotIsTaken)
        {
            InventorySlot selectedItem = Inventory.Instance.SelectedSlot;

            if (selectedItem.ObjectData.ItemCategory == "Seeds")
            {
                m_PlayerOnObject = true;
                ShowButtonPanel(true);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_CurrentState == State.Plowed)
            {
           
                m_PlayerOnObject = true;
                ShowButtonPanel(true);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = false;
            ShowButtonPanel(false);
        }
    }
}
