﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salesman : Npc
{
    public void OpenShop()
    {
        Store.Instance.OpenStorePanel(); // Open the store panel
    }
}
