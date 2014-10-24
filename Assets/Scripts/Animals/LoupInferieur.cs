using UnityEngine;
using System.Collections;

public class LoupInferieur : Loup
{
    public float distanceAlpha = 29;
    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("LoupBeta.Start");
        MindLoup mind = new MindLoupInferieur(this);
        base.construct(mind);
    }
}