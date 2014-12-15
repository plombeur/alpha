using UnityEngine;
using System.Collections;

public abstract class Loup : Animal
{

    public static bool GESTION_FAIM = true;
    public int FAIM_MAX;
    public float faim;

    public static bool getGESTION_FAIM()
    {
        return GESTION_FAIM;
    }

    public static void setGESTION_FAIM(bool value)
    {
        GESTION_FAIM = value;
    }

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

    public override System.Collections.Generic.List<SoundInformation> getSonsInterpellant()
    {
        return new System.Collections.Generic.List<SoundInformation>();
    }
}