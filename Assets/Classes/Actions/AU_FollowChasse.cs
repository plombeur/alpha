using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_FollowChasse : A_ActionUser
{
    private LoupAlpha alpha;
    private Vector3 tailleInitiale;
    private bool goAtk = false;

    protected override bool onStart(float deltaTime)
    {
        tailleInitiale = getAnimal().transform.localScale;
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return base.onStart(deltaTime);
    }

    protected override bool onResume(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return base.onResume(deltaTime);
    }

    public AU_FollowChasse(LoupAlpha alpha)
        : base("A_FollowChasse")
    {
        this.alpha = alpha;
    }

    protected override bool onUpdate(float deltaTime)
    {

        AU_Chasse actionChasse = alpha.getCurrentAction() as AU_Chasse;
        if(actionChasse == null)
        {
            getAnimal().transform.localScale = tailleInitiale;
            getActionPendlingList().removeAction(this);
            return false;
        }
        else
        {
            Animal animal = getAnimal();
            if ( actionChasse.target != null )
            {
                if (!goAtk && Vector2.Distance(actionChasse.target.transform.position, animal.transform.position) <= .25f)
                    goAtk = true;

                if(goAtk)
                {
                    if (animal.animationAttaque(actionChasse.target, animal.getFaceToDirection(actionChasse.target.transform.position)))
                    {
                        getAnimal().GetComponentInChildren<Voice>().makeSound(getAnimal().getIdentity(), getAnimal().attackSound);
                        actionChasse.target.blesse(10);
                        goAtk = false;
                    }
                }
                else
                {
                    animal.transform.localScale = tailleInitiale;
                    animal.setAgentToDontDodge(actionChasse.target);
                    animal.faceTo(actionChasse.target);
                    animal.fd(3 * animal.vitesse,true,false);
                }
            }
            else
            {
                animal.transform.localScale = tailleInitiale;
                goAtk = false;
                animal.faceTo(alpha);
                animal.fd();
            }
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        AU_FollowChasse action = obj as AU_FollowChasse;
        return action != null;
    }
}