using UnityEngine;
using System.Collections;

public class LoupOmega : Loup
{
    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("LoupOmega.Start");
        MindLoup mind = new MindLoupOmega(this);
        base.construct(mind);
    }
}