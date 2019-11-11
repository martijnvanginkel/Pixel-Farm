using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : InteractableObject
{
    //[SerializeField] private List<FishData> m_CommonFish = new List<FishData>();
    //private List<FishData> m_UnCommonFish = new List<FishData>();
    //private List<FishData> m_RareFish = new List<FishData>();

    private bool m_PlayerIsFishing;

    [SerializeField] private GameObject m_FishingBarParent;
    [SerializeField] private FishingBar m_FishingBar;

    private float m_WaitForFishTime = 5f;

    [SerializeField] private GameObject m_RandomFishPrefab;
    [SerializeField] private Transform m_FishSpawnPoint;

    protected override void Start()
    {
        base.Start();

        m_FishingBarParent.SetActive(false);
    }

    private void Update()
    {
        if (m_PlayerIsFishing)
        {
            CheckForInterupting();
            WaitForFish();
        }
    }

    private void CheckForInterupting()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            StopFishing();
        }
    }

    public void StartFishing()
    {
        m_PlayerIsFishing = true;
        PlayerController.Instance.Fish(true);
        //PlayerController.Instance.Talk("...", m_WaitForFishTime);

        ShowButtonPanel(false);
        base.PlayerActionEvent();
    }

    public void StopFishing()
    {
        m_FishingBarParent.SetActive(false);
        m_PlayerIsFishing = false;
        PlayerController.Instance.Fish(false);
        ShowButtonPanel(true);
    }

    private void WaitForFish()
    {
        m_WaitForFishTime -= Time.deltaTime;

        if (m_WaitForFishTime <= 0f)
        {
            m_PlayerIsFishing = false;
            m_WaitForFishTime = Random.Range(2.5f, 7.5f);

            // Start minigame
            m_FishingBarParent.SetActive(true);
            m_FishingBar.OpenFishGame();
        }
    }

    public void CatchFish()
    {
        Instantiate(m_RandomFishPrefab, m_FishSpawnPoint);

        // CATCHING FISH
        // float rareness = Random.Range(0f, 1f);

        // // This needs to be changed into a different chance for the type of fish that is being catched

        // if(rareness > 0f)
        // {
        //     int randomFishInt = Random.Range(0, m_CommonFish.Count);
        //     FishData randomFish = m_CommonFish[randomFishInt];

        //     Inventory.Instance.AddItem(randomFish, 1);
        // }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = true;
            ShowButtonPanel(true);
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
