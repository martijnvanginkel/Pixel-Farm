using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Food
{
    [SerializeField] private List<Sprite> m_MushroomSprites = new List<Sprite>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        PickRandomSprite();
    }

    private void PickRandomSprite()
    {
        int randomInt = Random.Range(0, m_MushroomSprites.Count);

        m_SpriteRenderer.sprite = m_MushroomSprites[randomInt];
    }

}
