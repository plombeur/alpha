using UnityEngine;
using System.Collections;

public class Ronce : Plant {

    GameObject seeds[];

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("Ronce.Start");
        MindRonce mind = new MindRonce(this);
        base.construct(mind, 1000);
    }

    public override void grow()
    {

    }
}
