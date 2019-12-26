using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : InteractableObject
{
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
    }

    public override void QuickAction()
    {
        Debug.Log("override");
        StartFishing();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_PlayerOnObject = true;
            ShowButtonPanel(true);
            PlayerController.Instance.SetOpenPanelObject(this);
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
