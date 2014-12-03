using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_FollowAlpha : A_ActionUser
{
    private float timer = (float)Random.Range(10, 30) * .1f;
    private float time = 0;
    private float cptNouvelleTrajectoire = 0;
    private float timeRecul = 0;
    private Living tmpLoupToDodge;

    public AU_FollowAlpha()
        : base("AU_MoveTo")
    {}

    protected override bool onUpdate(float deltaTime)
    {
        LoupAlpha alpha = (LoupAlpha)GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();

        if(alpha.getCurrentAction() as AU_MoveTo == null)
        {
            if (timer <= 0)
            {
                getActionPendlingList().removeAction(this);
                return false;
            }

            timer -= deltaTime;

            getAnimal().wiggle(getAnimal().vitesse,3);

            return true;
        }

        if(Vector2.Distance(getAnimal().transform.position,alpha.transform.position)>1)
        {
            getAnimal().setAgentToDontDodge(alpha);
            getAnimal().faceTo(alpha.transform.position);
            getAnimal().wiggle(getAnimal().vitesse, 2);
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        AU_FollowAlpha action = obj as AU_FollowAlpha;
        return action != null;
    }

    protected override bool onStart(float deltaTime)
    {
        getAnimal().GetComponent<SpriteRenderer>().sprite = getAnimal().normalSprite;
        return base.onStart(deltaTime);
    }
}

