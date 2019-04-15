using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : InteractableObject
{
    [SerializeField] private List<FishData> m_CommonFish = new List<FishData>();
    private List<FishData> m_UnCommonFish = new List<FishData>();
    private List<FishData> m_RareFish = new List<FishData>();

    private bool m_PlayerIsFishing;

    private float m_WaitForFishTime = 3f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (m_PlayerIsFishing)
        {
            WaitForFish();
        }
    }

    private void WaitForFish()
    {
        m_WaitForFishTime -= Time.deltaTime;

        if(m_WaitForFishTime <= 0f)
        {
            m_PlayerIsFishing = false;
            m_WaitForFishTime = 3f;
            CatchFish();
            PlayerController.Instance.Fish(false);
            ShowButtonPanel(true);
        }
    }

    private void CatchFish()
    {
        float randomFloat = Random.Range(0f, 1f);

        //if(randomFloat < 0.1)


        Debug.Log(randomFloat);
    }

    public void StartFishing()
    {
        m_PlayerIsFishing = true;
        PlayerController.Instance.Fish(true);
        ShowButtonPanel(false);
        base.PlayerActionEvent();
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
