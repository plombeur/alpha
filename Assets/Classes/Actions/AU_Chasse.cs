using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_Chasse : A_ActionUser
{
    public Vector3 targetPosition;
    private GameObject ciblePosition;
    public Animal target;
    private GameObject cible;
    public Vector3 tailleInitiale;
    private bool goAtk = false;
    private float timeRugissement = 1.2f;
    private float atkDelay = 0;

    protected override bool onStart(float deltaTime)
    {
        ciblePosition = (GameObject)GameObject.Instantiate(((LoupAlpha)getAnimal()).prefabTarget);
        ciblePosition.transform.localScale = ciblePosition.transform.localScale * .5f;
        tailleInitiale = getAnimal().transform.localScale;
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return onUpdate(deltaTime);
    }

    protected override void onPause()
    {
        if (ciblePosition != null)
            GameObject.Destroy(ciblePosition.gameObject);

        if (cible != null)
            GameObject.Destroy(cible.gameObject);
        getAnimal().hideStaticEmoticon();
        base.onPause();
    }

    protected override void onRemove()
    {
        if (ciblePosition != null)
            GameObject.Destroy(ciblePosition.gameObject);

        if(cible != null)
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
        if (atkDelay > 0)
            atkDelay -= deltaTime;

        if (ciblePosition != null)
        {
            targetPosition.z = 50;
            ciblePosition.transform.position = targetPosition;
        }

        Animal animal = getAnimal();

        if (timeRugissement > 0)
        {
            getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().rugirSprite;
            timeRugissement -= Time.deltaTime;
            if (timeRugissement <= 0)
                getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
            return true;
        }

        if (target != null && !target.targetable())
        {
            animal.transform.localScale = tailleInitiale;
            GameObject.Destroy(cible.gameObject);
            cible = null;
            target = null;
            goAtk = false;
        }

        if (target == null)
        {
            List<Living> livings = animal.perceptView.getLiving();
            for(int i=0;i<livings.Count;++i)
                if ( ( livings[i] as Sheep != null || livings[i] as Rabbit != null || livings[i] as Ours != null) && ((Animal)livings[i]).targetable())
                {
                    target = (Animal)livings[i];
                    cible = ((LoupAlpha)animal).cibler(livings[i]);
                    break;
                }
        }
        
        if(target == null)
        {
            animal.transform.localScale = tailleInitiale;
            if(Vector2.Distance(getAnimal().transform.position, targetPosition) <= .25f)
            {
                getActionPendlingList().removeAction(this);
                return true;
            }
            animal.faceTo(targetPosition);
            animal.fd(animal.vitesse * 1.5f);
        }
        else
        {
            if (Vector2.Distance(animal.transform.position, target.transform.position) <= .25f)
            {
                goAtk = true;
            }
                
            if(goAtk)
            {
                if(atkDelay > 0)
                {
                    animal.faceTo(target);
                    animal.fd(target.vitesse,false,false);
                }
                else if (atkDelay <= 0 && animal.animationAttaque(target, animal.getFaceToDirection(target.transform.position)))
                {
                    atkDelay = 1;
                    target.blesse(10);
                    if (target.estMort())
                    {
                        GameObject.Destroy(cible.gameObject);
                        cible = null;
                        target = null;
                    }
                    goAtk = false;
                }
            }
            else
            {
                animal.transform.localScale = tailleInitiale;
                animal.faceTo(target);
                animal.fd(animal.vitesse * 3, true, false);
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

    public AU_MoveTo getActionMoveToConverti()
    {
        return new AU_MoveTo(targetPosition);
    }
}