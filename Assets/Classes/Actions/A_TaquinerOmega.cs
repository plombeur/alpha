﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_TaquinerOmega : Action
{
    private LoupOmega cible;
    private bool goAtk = false;
    private bool goRetourAtk = false;
    private Vector3 tailleInitiale;

    public A_TaquinerOmega(LoupOmega cible)
        : base("A_TaquinerOmega")
    {
        this.cible = cible;
    }

    public override float getPriority()
    {
        return 0.1f;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override bool onUpdate(float deltaTime)
    {
        Animal a = getAnimal();
        if(!goAtk && Vector2.Distance(a.GetComponent<Transform>().position,cible.GetComponent<Transform>().position) <= 5)
        {
            goAtk = true;
            tailleInitiale = getAnimal().transform.localScale;
        }

        if(goAtk)
        {
            if(!goRetourAtk)
            {
                getAnimal().transform.localScale += new Vector3(.7f,.7f,.7f);
                if (getAnimal().transform.localScale.x >= tailleInitiale.x * 3)
                    goRetourAtk = true;
            }
            else
            {
                getAnimal().transform.localScale -= new Vector3(.4f, .4f, .4f);
                if (getAnimal().transform.localScale.x <= tailleInitiale.x)
                {
                    getAnimal().transform.localScale = tailleInitiale;
                    if (cible.getCurrentAction() as A_Repos != null)
                        ((MindAnimal)cible.mind).removeCurrentAction();
                    getActionPendlingList().removeAction(this);
                }

            }

            return true;
        }

        a.setAgentToDontDodge(cible);
        a.faceTo(cible);
        a.wiggle(getAnimal().vitesse, 2);
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_TaquinerOmega action = obj as A_TaquinerOmega;
        return action != null && cible == action.cible;
    }
}

