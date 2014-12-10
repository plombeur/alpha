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

    public override float getDirectionFuite()
    {
        throw new System.NotImplementedException();
    }

    public override bool besoinDeFuir()
    {
        return false;
    }

    protected override void onCreate()
    {
        //A implementer quand des animaux feront peur aux loups
        throw new System.NotImplementedException();
    }
}