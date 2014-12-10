using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rabbit : Animal 
{

    protected override void onCreate()
    {
        MindRabbit mind = new MindRabbit(this);
        base.construct(mind);
    }

    public override void construct(Mind mind)
    {
        base.construct(new MindRabbit(this));
    }

    public override bool besoinDeFuir()
    {
        List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(GetComponent<Memory>().getMemoyBlocs());
        for (int i = 0; i < memoryBlocs.Count; ++i)
        {
            Loup currentLoup = memoryBlocs[i].getEntity() as Loup;
            Sheep currentSheep = memoryBlocs[i].getEntity() as Sheep;
            if (currentLoup != null || currentSheep != null)
            {
                Vector2 lastPosition = memoryBlocs[i].getLastPosition();
                float distance = Vector2.Distance(lastPosition, transform.position);
                if (distance < distanceDeSecurite)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override float getDirectionFuite()
    {
        List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(GetComponent<Memory>().getMemoyBlocs());
        List<Vector2> vecteursDeFuite = new List<Vector2>();
        List<float> poids = new List<float>();
        for (int i = 0; i < memoryBlocs.Count; ++i)
        {
            Loup currentLoup = memoryBlocs[i].getEntity() as Loup;
            Sheep currentSheep = memoryBlocs[i].getEntity() as Sheep;
            if (currentLoup != null || currentSheep != null)
            {
                Vector2 lastPosition = memoryBlocs[i].getLastPosition();
                float distance = Vector2.Distance(lastPosition, transform.position);
                if (distance < distanceDeSecurite)
                {
                    vecteursDeFuite.Add(Utils.vectorFromAngle(getFaceToDirection(lastPosition) + 180));
                    poids.Add(1 - distance / distanceDeSecurite);
                }
            }
        }
        if (vecteursDeFuite.Count == 0)
            return -1;
        else
        {
            Vector2 somme = new Vector2(0, 0);
            for (int i = 0; i < vecteursDeFuite.Count; ++i)
                somme += poids[i] * vecteursDeFuite[i];
            return Utils.angleFromVector(somme);
        }
    }

}
