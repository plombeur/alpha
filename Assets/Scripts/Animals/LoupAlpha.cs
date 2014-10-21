using UnityEngine;
using System.Collections;

public class LoupAlpha : Loup
{

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("LoupAlpha.Start");
        MindLoup mind = new MindLoupAlpha(this);
        base.construct(mind);
    }
}