﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_RejoindreTroupe : Action
{
    private float vitesse;
    private float cptNouvelleTrajectoire = 0;
    private float time = 0;

    public A_RejoindreTroupe()
        : base("A_RejoindreTroupe")
    {}

    public override float getPriority()
    {
        return 1;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        if (Living.DEBUG)
            Debug.Log("A_RejoindreTroupe ...");
        /**** CODE A MODIFIER QUAND LES PERCEPTS SERONT FONCTIONNELS *****/
        LoupAlpha alpha = GameObject.Find("AlphaWolf").GetComponent<LoupAlpha>();
        PerceptView percepts = getAnimal().perceptView;
        List<Living> list = percepts.getLiving();
        if(list.Count>0)
        {
            getActionPendlingList().removeAction(this);
            return false;
        }

        getAnimal().rt(1);
        getAnimal().fd(0.01f);
        
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_RejoindreTroupe action = obj as A_RejoindreTroupe;
        return action != null;
    }
}