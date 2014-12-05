using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// SHEEP ONLY
public class A_PromenadeEtFlocking : Action
{
    private float cptNouvelleTrajectoire = 0;

    public A_PromenadeEtFlocking() : base("A_PromenadeEtFlocking")
    {}

    public override float getPriority()
    {
        return 0;
    }

    protected override bool onResume(float deltaTime)
    {
        cptNouvelleTrajectoire = 0;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        cptNouvelleTrajectoire -= deltaTime;
        List<Vector2> positionsDesMoutons = new List<Vector2>();
        List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(getAnimal().GetComponent<Memory>().getMemoyBlocs());
        bool tropProche = false;
        for(int i=0;i<memoryBlocs.Count;++i)
        {
            Sheep currentSheep = memoryBlocs[i].getEntity() as Sheep;
            Vector2 lastPosition = memoryBlocs[i].getLastPosition();
            if(currentSheep != null && currentSheep != getAnimal())
            {
                if(Vector2.Distance(getAnimal().transform.position,lastPosition) <= 1.5f)
                {
                    tropProche = true;
                    break;
                }
                positionsDesMoutons.Add(currentSheep.transform.position);
            }
        }
        float randomDirection = Random.Range(0, 360);
        if(tropProche || positionsDesMoutons.Count == 0)
        {
            if(cptNouvelleTrajectoire <= 0)
            {
                getAnimal().direction = randomDirection;
                cptNouvelleTrajectoire = Random.Range(7, 25) - cptNouvelleTrajectoire;
            }
            getAnimal().fd();
            return true;
        }
        else
        {
            Vector2 moyennePosition = positionsDesMoutons[0];
            for (int i = 1; i < positionsDesMoutons.Count; ++i)
                moyennePosition = (positionsDesMoutons[i] + moyennePosition) * .5f;

            if (cptNouvelleTrajectoire <= 0)
            {
                cptNouvelleTrajectoire = Random.Range(7, 25) - cptNouvelleTrajectoire;
            }
            else
                randomDirection = getAnimal().direction;

            getAnimal().direction = Utils.angleFromVector(Utils.vectorFromAngle(randomDirection) + Utils.vectorFromAngle(getAnimal().getFaceToDirection(moyennePosition)));
            getAnimal().fd();
            return true;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_PromenadeEtFlocking action = obj as A_PromenadeEtFlocking;
        return action != null;
    }
}
