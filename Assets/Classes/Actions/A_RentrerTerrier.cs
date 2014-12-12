using UnityEngine;
using System.Collections;

public class A_RentrerTerrier : Action
{

    private float duree;
    private float time = 0;
    private bool dansLeTerrier = false;

    public A_RentrerTerrier(float duree)
        : base("A_RentrerTerrier")
    {
        this.duree = duree;
    }

    public override float getPriority()
    {
        return 0.2f;
    }

    protected override bool onStart(float deltaTime)
    {
        return base.onStart(deltaTime);
    }
    protected override bool onResume(float deltaTime)
    {
        return base.onStart(deltaTime);
    }


    protected override bool onUpdate(float deltaTime)
    {
        Rabbit rabbit = getAnimal() as Rabbit;
        if( rabbit == null )
        {
            Debug.LogError("A_RentrerTerrier doit être donné a un Lapin ! :x");
        }

        if(rabbit.aCoteDuTerrier())
        {
            rabbit.GetComponent<SpriteRenderer>().enabled = false;
            time += deltaTime;
            if(time >= duree)
            {
                rabbit.GetComponent<SpriteRenderer>().enabled = true;
                getActionPendlingList().removeAction(this);
                return true;
            }
            return true;
        }

        rabbit.GetComponent<SpriteRenderer>().enabled = true;
        rabbit.faceTo(rabbit.trou.transform.position);
        rabbit.fd(rabbit.vitesse);

        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_RentrerTerrier action = obj as A_RentrerTerrier;
        return action != null;
    }
}
