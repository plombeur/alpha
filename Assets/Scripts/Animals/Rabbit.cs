using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rabbit : Animal 
{
    public GameObject trou;

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
        float distanceMin = float.MaxValue;
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
                    if (distance < distanceMin)
                        distanceMin = distance;
                    vecteursDeFuite.Add(Utils.vectorFromAngle(getFaceToDirection(lastPosition) + 180));
                    poids.Add(1 - distance / distanceDeSecurite);
                }
            }
        }
        if (vecteursDeFuite.Count == 0)
            return getFaceToDirection(trou.transform.position);
        else
        {
            float faceToTrou = getFaceToDirection(trou.transform.position);
            if (distanceMin > Vector2.Distance(trou.transform.position, transform.position) )
                return faceToTrou;
            Vector2 somme = new Vector2(0, 0);
            for (int i = 0; i < vecteursDeFuite.Count; ++i)
                somme += poids[i] * vecteursDeFuite[i];
            if (Vector2.Angle(somme, Utils.vectorFromAngle(faceToTrou)) <= 90)
                return faceToTrou;
            else
                return Utils.angleFromVector(somme);
        }
    }

    public bool aCoteDuTerrier()
    {
        return Vector2.Distance(transform.position, trou.transform.position) <= .3f;
    }

    public bool dansLeTerrier()
    {
        return Vector2.Distance(transform.position, trou.transform.position) <= .3f && !GetComponent<SpriteRenderer>().enabled;
    }

    public override System.Collections.Generic.List<SoundInformation> getSonsInterpellant()
    {
        System.Collections.Generic.List<SoundInformation> sons =  new System.Collections.Generic.List<SoundInformation>();
        sons.Add(SoundInformation.WolfWalk);
        return sons;
    }

}
