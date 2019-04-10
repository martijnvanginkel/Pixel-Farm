using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Plant
{
    [SerializeField] private GameObject m_MushroomPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        DayManager.OnEndOfDay += GrowMushroom;
    }

    private void OnDisable()
    {
        DayManager.OnEndOfDay -= GrowMushroom;
    }

    private void GrowMushroom()
    {
        Instantiate(m_MushroomPrefab, new Vector3(RandomSpawnLocation(), transform.position.y, 0), transform.rotation);
    }

    private float RandomSpawnLocation()
    {
        float randomFloat = Random.Range(-0.5f, 0.5f);

        return transform.position.x + randomFloat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
