using UnityEngine;
using System.Collections;

public abstract class LoupInferieur : Loup
{
    public float distanceAlpha = 29;
    public static LoupAlpha alpha = null;

    public override void construct(Mind mind)
    {
        if (DEBUG)
            Debug.Log("LoupInferieur.Start");
        if (alpha == null)
            alpha = GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();
        base.construct(mind);
    }
}