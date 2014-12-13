using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MindAnimal : Mind
{
    protected ActionPendingList actionList;

    public MindAnimal(Animal agent) : base(agent)
    {
        actionList = new ActionPendingList();
        actionList.setAnimal(agent);
    }
    public override void vivre()
    {
        Animal animal = (Animal)agent;

        if (animal.estMort())
            return;

        animal.resetAgentToDontDodge();
        animal.fd(0);


        if (agent.DEBUG)
            Debug.Log("** Action en cours : " + actionList.getActualAction());

        //Gestion des reflexes, fuite automatique
        if ( animal.peutAvoirPeur && animal.besoinDeFuir() )
        {
            actionList.addAction(new A_Fuite());
        }

        //Gestion des bruits qui interpellent l'agent.
        if (animal.perceptHearing != null)
        {
            List<SoundPercepted> sounds = animal.perceptHearing.getSounds();
            List<SoundInformation> sonQuiInterpellent = animal.getSonsInterpellant();
            if (sounds.Count > 0)
            {
                for (int i = 0; i < sounds.Count; ++i)
                {
                    if (animal.getSonsInterpellant().Contains(sounds[i].soundInformation))
                    {
                        actionList.addAction(new A_RegarderVersLeBruit(sounds[i].identity));
                    }
                }
            }
        }

        actionList.execute(Time.deltaTime);
    }

    public Action getCurrentAction()
    {
        return actionList.getActualAction();
    }

    public void removeCurrentAction()
    {
        actionList.removeAction(actionList.getActualAction());
    }
}