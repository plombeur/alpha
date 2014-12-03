using UnityEngine;
using System.Collections;

public abstract class LoupInferieur : Loup
{
    public float distanceAlpha = 29;
    public override void construct(Mind mind)
    {
        if (Living.DEBUG)
            Debug.Log("LoupInferieur.Start");
        base.construct(mind);
    }
}