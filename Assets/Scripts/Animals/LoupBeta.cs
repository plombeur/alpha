﻿using UnityEngine;
using System.Collections;

public class LoupBeta : LoupInferieur
{
    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("LoupBeta.Start");
        MindLoup mind = new MindLoupBeta(this);
        base.construct(mind);
    }
}