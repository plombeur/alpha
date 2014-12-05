using UnityEngine;
using System.Collections;

public class LoupOmega : LoupInferieur
{
    protected override void onCreate()
    {
        if (DEBUG)
            Debug.Log("LoupOmega.Start");
        MindLoup mind = new MindLoupOmega(this);
        base.construct(mind);
    }
}