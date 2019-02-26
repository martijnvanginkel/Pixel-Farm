using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wife : ReceiveableObject
{

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ReceiveItem()
    {
        base.ReceiveItem();
        base.StartCoroutine("OpenTextBalloon", "Thank you!");
    }
}
