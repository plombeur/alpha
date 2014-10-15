using UnityEngine;
using System.Collections;

public class Ronce : Plant {

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("Ronce.Start");
        MindLoup mind = new MindRonce(this);
        base.construct(mind, 100);
    }
}
