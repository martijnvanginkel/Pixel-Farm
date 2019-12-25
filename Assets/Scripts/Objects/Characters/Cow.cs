using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{

    private LayerMask m_TileLayer;
    private bool m_IsMilkedToday;
    [SerializeField] private ObjectData m_MilkGlassData;
    [SerializeField] private TMPro.TextMeshProUGUI m_TextBubble;

    private void OnEnable()
    {
        DayManager.OnEndOfDay += ResetCowNipple;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfDay -= ResetCowNipple;
    }

    protected override void Start()
    {
        base.Start();

        m_TileLayer = LayerMask.GetMask("Tile");
    }

    protected override void ResetTimer()
    {
        base.ResetTimer();
        base.MoveAnimal();
    }

    public void Milk()
    {
        ShowButtonPanel(false);
        if (m_IsMilkedToday)
        {
            Talk("I'm empty", 2f);
        }
        else
        {
            Talk("That feels nice", 2f);
            Inventory.Instance.AddItem(m_MilkGlassData, 1);
            m_IsMilkedToday = true;
        }
    }

    private void ResetCowNipple()
    {
        m_IsMilkedToday = false;
    }

    private void Talk(string text, float length)
    {
        StartCoroutine(TalkCo(text, length));
    }

    private IEnumerator TalkCo(string text, float length)
    {
        m_TextBubble.text = text;
        m_TextBubble.enabled = true;
        yield return new WaitForSeconds(length);
        m_TextBubble.enabled = false;
        m_TextBubble.text = "";
    }

    //private void EatGrass()
    //{
    //    GameObject standingTile = FindStandingTile();
    //    GrassTile grassTile = standingTile.GetComponent<GrassTile>();

    //    if (grassTile.IsTileOnDefault())
    //    {
    //        grassTile.Cut();
    //    }
    //}

    // Finds the tile the cow is currently standing on
    private GameObject FindStandingTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_TileLayer);

        return hit.collider.gameObject;
    }

    // Gets triggered a second into the cows idle animation
    private void SecondInIdleAnimation()
    {
        //if (base.RandomBool(1f)) // Chance of eating grass
        //{
        //    EatGrass();
        //}
    }
}
