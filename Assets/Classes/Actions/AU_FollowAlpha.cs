using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AU_FollowAlpha : A_ActionUser
{

    private float timerBlocked = 0;
    private float timer = Random.Range(1, 6);
    private float time = 0;
    private float cptNouvelleTrajectoire = 0;

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
            if(currentLoup != null&& (getAnimal().direction - getAnimal().getFaceToDirection(currentLoup.transform.position))%360 < 40)
            {
                if (loupLePlusProche == null || Vector2.Distance(currentLoup.transform.position, getAnimal().transform.position) < Vector2.Distance(loupLePlusProche.transform.position, getAnimal().transform.position))
                {
                    loupLePlusProche = currentLoup;
                }
            }
        }

        if (timerBlocked>3 || loupLePlusProche == null || Vector2.Distance(loupLePlusProche.transform.position, getAnimal().transform.position) > 5)
        {
            getAnimal().faceTo(alpha.GetComponent<Transform>().position);
            getAnimal().wiggle(getAnimal().vitesse, 2);
            if (timerBlocked < 3)
                timerBlocked = 0;
        }
        else
        {
            timerBlocked += deltaTime;
            getAnimal().fd(getAnimal().vitesse * .01f);
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
}

