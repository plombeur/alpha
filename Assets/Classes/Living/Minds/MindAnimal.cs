using System;
using System.Collections;
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
        Animal animal = (Animal) agent;
        animal.faim -= Time.deltaTime;
        if(animal.faim < 0)
        {
            animal.vie += animal.faim;
            if(animal.vie <= 0)
            {
                animal.vie = 0;
                if (Living.DEBUG)
                    Debug.Log("Mort.");
            }
            animal.faim = 0;
        }
        animal.fd(0);
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