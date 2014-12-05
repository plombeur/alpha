﻿using UnityEngine;
using System.Collections;

public class LoupBeta : LoupInferieur
{
    public float THREAT_MAX = 100;
    public float threat = 0;
    private float aggressivite;

    protected override void onCreate()
    {
        if (DEBUG)
            Debug.Log("LoupBeta.Start");
        aggressivite = THREAT_MAX / Random.Range(40,210);
        MindLoup mind = new MindLoupBeta(this);
        base.construct(mind);
    }

    public float getAggressivite()
    {
        return aggressivite;
    }
}