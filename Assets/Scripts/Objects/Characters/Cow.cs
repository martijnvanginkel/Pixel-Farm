using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{
    protected override void ResetTimer()
    {
        base.ResetTimer();
        base.MoveAnimal();
    }
}
