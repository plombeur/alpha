using UnityEngine;
using System.Collections;

public abstract class Loup : Animal
{

    public int FAIM_MAX;
    public float faim;

    //Pour la chasse
    public GameObject prefabTarget;

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

    public GameObject cibler(Living living)
    {
        GameObject cible = (GameObject)GameObject.Instantiate(prefabTarget);
        cible.transform.parent = living.transform;
        cible.GetComponent<FollowTarget>().target = living.gameObject;
        return cible;
    }
}