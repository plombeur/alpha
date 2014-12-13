﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_Chasse : A_ActionUser
{
    public Vector2 targetPosition;
    private Animal target;
    private GameObject cible;
    public Vector3 tailleInitiale;
    private bool goAtk = false;

    protected override bool onStart(float deltaTime)
    {
        tailleInitiale = getAnimal().transform.localScale;
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override void onPause()
    {
        GameObject.Destroy(cible.gameObject);
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override void onRemove()
    {
        GameObject.Destroy(cible.gameObject);
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    public AU_Chasse(Vector2 targetPosition)
        : base("AU_Chasse")
    {
        this.targetPosition = targetPosition;
    }

    protected override bool onUpdate(float deltaTime)
    {
        Animal animal = getAnimal();

        if (target != null && !target.targetable())
        {
            GameObject.Destroy(cible.gameObject);
            cible = null;
            target = null;
            goAtk = false;
        }

        if (target == null)
        {
            List<Living> livings = animal.perceptView.getLiving();
            for(int i=0;i<livings.Count;++i)
                if ( ( livings[i] as Sheep != null || livings[i] as Rabbit != null ) && ((Animal)livings[i]).targetable())
                {
                    target = (Animal)livings[i];
                    cible = ((Loup)animal).cibler(livings[i]);
                    break;
                }
        }
        
        if(target == null)
        {
            animal.faceTo(targetPosition);
            animal.fd();
        }
        else
        {
            if (Vector2.Distance(animal.transform.position, target.transform.position) <= .25f)
            {
                goAtk = true;
            }
                
            if(goAtk)
            {
                if (animal.animationAttaque(target, tailleInitiale))
                {
                    Debug.Log("Blesse!");
                    target.blesse(10);
                    if (target.estMort())
                    {
                        GameObject.Destroy(cible.gameObject);
                        cible = null;
                        target = null;
                        goAtk = false;
                    }
                }
            }
            else
            {
                animal.setAgentToDontDodge((Animal)target);
                animal.faceTo(target);
                animal.fd(animal.vitesse * 3);
            }
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        AU_Chasse action = obj as AU_Chasse;
        return action != null;
    }
}