using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_Chasse : A_ActionUser
{
    private Vector2 targetPosition;
    private Animal target;
    private GameObject cible;

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override void onPause()
    {
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override void onRemove()
    {
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
            animal.setAgentToDontDodge((Animal)target);
            animal.faceTo(target);
            animal.fd(animal.vitesse * 3);
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