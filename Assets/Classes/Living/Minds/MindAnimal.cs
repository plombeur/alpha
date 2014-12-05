﻿using System;
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
        Animal animal = (Animal)agent;
        animal.resetAgentToDontDodge();
        animal.fd(0);
        if (agent.DEBUG)
            Debug.Log("** Action en cours : " + actionList.getActualAction());
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