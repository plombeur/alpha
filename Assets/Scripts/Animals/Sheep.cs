using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sheep : Animal
{

    protected override void onCreate()
    {
        MindSheep mind = new MindSheep(this);
        base.construct(mind);
    }

    /*
    * Retourne -1 si aucun loup n'est à une portée dangeureuse
    */
    public override bool besoinDeFuir()
    {
        List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(GetComponent<Memory>().getMemoyBlocs());
        for (int i = 0; i < memoryBlocs.Count; ++i)
        {
            Loup currentLoup = memoryBlocs[i].getEntity() as Loup;
            if (currentLoup != null)
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
            if (currentLoup != null && !currentLoup.estMort() )
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

    public override System.Collections.Generic.List<SoundInformation> getSonsInterpellant()
    {
        System.Collections.Generic.List<SoundInformation> sons =  new System.Collections.Generic.List<SoundInformation>();
        sons.Add(SoundInformation.WolfWalk);
        return sons;
    }
}