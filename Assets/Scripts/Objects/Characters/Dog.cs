using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void ResetTimer()
    {
        base.ResetTimer();
        base.MoveAnimal();
    }
}
