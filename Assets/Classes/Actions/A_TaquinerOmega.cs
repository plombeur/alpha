using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_TaquinerOmega : Action
{
    private LoupOmega cible;
    private bool goAtk = false;
    private float goInCpt = 0.1f;

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
        }
        else
        {
            if(goAtk)
            {
                if(goInCpt<=0)
                {
                    goInCpt += deltaTime;
                    a.fd(a.vitesse * 30);
                    if (goInCpt >= 0)
                    {
                        getActionPendlingList().removeAction(this);
                        return true;
                    }
                }
                else
                {
                    goInCpt -= deltaTime;
                    a.fd(a.vitesse * 30);
                    if (goInCpt <= 0)
                    {
                        MindLoupOmega mindLoupOmega = (MindLoupOmega)cible.mind;
                        if (mindLoupOmega.getCurrentAction() as A_Repos != null)
                            mindLoupOmega.removeCurrentAction();
                        goInCpt = -0.1f;
                        a.rt(180);
                    }
                }
                return true;
            }
            a.faceTo(cible);
            a.wiggle(getAnimal().vitesse,2);
        }
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

