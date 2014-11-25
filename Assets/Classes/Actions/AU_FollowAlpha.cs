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
    {
    }

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

            getAnimal().fd();

            return true;
        }

        //Gestion de l'approchement des loups
        List<Living> percepts = getAnimal().perceptView.getLiving();
        Loup loupLePlusProche = null;
        Loup currentLoup;
        for (int i = 0; i < percepts.Count;++i)
        {
            currentLoup = percepts[i] as Loup;
            if(currentLoup != null && (getAnimal().direction - getAnimal().getFaceToDirection(currentLoup.transform.position))%360 < 45)
            {
                if (loupLePlusProche == null || Vector2.Distance(currentLoup.transform.position, getAnimal().transform.position) < Vector2.Distance(loupLePlusProche.transform.position, getAnimal().transform.position))
                {
                    loupLePlusProche = currentLoup;
                }
            }
        }

        if ( timeRecul <= 0 && loupLePlusProche != null && Vector2.Distance(loupLePlusProche.transform.position, getAnimal().transform.position) <= 8)
        {
            timeRecul = (float)Random.Range(0, 11) * .1f + .9f;
            tmpLoupToDodge = loupLePlusProche;
        }
        
        if(timeRecul <= 0)
        {
            getAnimal().faceTo(alpha.GetComponent<Transform>().position);
            getAnimal().wiggle(getAnimal().vitesse, 2);
        }
        else
        {
            timeRecul -= Time.deltaTime;
            getAnimal().faceTo(tmpLoupToDodge);
            getAnimal().rt(180);
            getAnimal().wiggle(getAnimal().vitesse * .9f, 3);
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

