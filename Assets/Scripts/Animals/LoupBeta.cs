using UnityEngine;
using System.Collections;

public class LoupBeta : LoupInferieur
{
    public float THREAT_MAX = 100;
    public float threat = 0;

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("LoupBeta.Start");
        MindLoup mind = new MindLoupBeta(this);
        base.construct(mind);
    }
}