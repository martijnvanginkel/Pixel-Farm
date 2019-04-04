using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private GameObject m_EggPrefab;

    protected override void ResetTimer()
    {
        base.ResetTimer();

        if (base.RandomBool(0.2f)) // Chance of laying an egg on movement
        {
            LayEgg();
        }

        base.MoveAnimal();
    }

    private void LayEgg()
    {
        Instantiate(m_EggPrefab, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
