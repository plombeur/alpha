using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MindLoup : MindAnimal {

    private float compteurActiviteAleatoire = Random.Range(0, 10);

    public MindLoup(Loup agent) : base(agent)
    { }
	public override void vivre()
    {
        base.vivre();

        Loup loup = ((Loup)agent);

        if (Loup.GESTION_FAIM)
        {
            if (loup.faim <= loup.FAIM_MAX / 2)
            {
                List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(loup.GetComponent<Memory>().getMemoyBlocs());
                Animal plusProcheCadavre = null;
                for (int i = 0; i < memoryBlocs.Count; ++i)
                {
                    Animal animal = memoryBlocs[i].getEntity() as Animal;
                    if (animal != null)
                    {
                        if (animal.estMort())
                        {
                            if (animal.quantiteDeViande > 0)
                            {
                                if (plusProcheCadavre == null)
                                {
                                    plusProcheCadavre = animal;
                                }
                                else if (Vector2.Distance(plusProcheCadavre.transform.position, agent.transform.position) > Vector2.Distance(animal.transform.position, agent.transform.position))
                                    plusProcheCadavre = animal;
                            }
                        }
                    }
                }

                if (plusProcheCadavre != null)
                {
                    A_SeNourrir newAction = new A_SeNourrir(plusProcheCadavre);
                    A_SeNourrir actionNourrirPrecedente = actionList.getFirstActionWithSameType<A_SeNourrir>();
                    if (actionNourrirPrecedente == null)
                        actionList.addAction(newAction);
                    else if (actionNourrirPrecedente.getDistanceFrom(agent.transform.position) > newAction.getDistanceFrom(agent.transform.position))
                    {
                        actionList.removeAction(actionNourrirPrecedente);
                        actionList.addAction(newAction);
                    }
                }
            }
            loup.faim -= Time.deltaTime;
            if (loup.faim <= loup.FAIM_MAX / 2)
            {
                loup.displayStaticEmoticon(loup.hungryEmoticonSprite);
            }
            if (loup.faim <= 0)
            {
                loup.blesse(-loup.faim);
                loup.faim = 0;
            }
        }

        actionList.addAction(new A_Promenade(((Animal)agent).vitesse));
        if (actionList.size() == 1)  //Si l'agent ne fait que se promener et qu'il s'embête ... ajout d'une action d'occupation aléatoire spécifiée par randomAction()
        {
            compteurActiviteAleatoire -= Time.deltaTime;
            if (compteurActiviteAleatoire <= 0)
            {
                randomAction();
                compteurActiviteAleatoire = Random.Range(10, 50);
            }
        }
    }

    protected virtual void randomAction()
    {
        actionList.addAction(new A_Repos(Random.Range(10, 50)));
    }

}