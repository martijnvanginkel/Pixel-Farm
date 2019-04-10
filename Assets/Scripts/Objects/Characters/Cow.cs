using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{
    private LayerMask m_TileLayer;

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

    private void EatGrass()
    {
        GameObject standingTile = FindStandingTile();
        GrassTile grassTile = standingTile.GetComponent<GrassTile>();

        if (grassTile.IsTileOnDefault())
        {
            grassTile.Cut();
        }
    }

    // Finds the tile the cow is currently standing on
    private GameObject FindStandingTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_TileLayer);

        return hit.collider.gameObject;
    }

    // Gets triggered a second into the cows idle animation
    private void SecondInIdleAnimation()
    {
        if (base.RandomBool(0.2f)) // Chance of eating grass
        {
            EatGrass();
        }
    }
}
