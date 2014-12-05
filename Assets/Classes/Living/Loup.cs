using UnityEngine;
using System.Collections;

public abstract class Loup : Animal
{

    public int FAIM_MAX;
    public float faim;

    public override void construct(Mind mind)
    {
        if (DEBUG)
            Debug.Log("Loup.Start");
        base.construct(mind);
	}
}