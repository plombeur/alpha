using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_SeNourrir : Action
{
    private Animal cadavre;
    public A_SeNourrir(Animal cadavre) : base("A_SeNourrir")
    {
        this.cadavre = cadavre;
    }

    public override float getPriority()
    {
        return 5;
    }

    protected override bool onUpdate(float deltaTime)
    {
        if (cadavre.quantiteDeViande <= 0 || ((Loup)getAnimal()).faim >= ((Loup)getAnimal()).FAIM_MAX )
        {
            getActionPendlingList().removeAction(this);
            return false;
        }

        bool canEat = false;
        if(getAnimal() as LoupAlpha != null)
        {
            if (getAnimal().DEBUG)
                Debug.Log("Le loup alpha peut se nourrir !");
            canEat = true;
        }
        else if(getAnimal() as LoupBeta != null && LoupInferieur.alpha.getCurrentAction() as A_SeNourrir != null)
        {
            if(getAnimal().DEBUG)
                Debug.Log("Le loup beta peut se nourrir !");
            canEat = true;
        }
        else if(getAnimal() as LoupOmega != null)
        {
            if(LoupInferieur.alpha.getCurrentAction() as A_SeNourrir == null)
            {
                List<Living> livings = getAnimal().perceptView.getLiving();
                int i;
                for (i = 0; i < livings.Count; ++i)
                    if (livings[i] as LoupBeta != null)
                    {
                        if (getAnimal().DEBUG)
                            Debug.Log("Loup beta vu, action : " + ((LoupBeta)livings[i]).getCurrentAction());
                        if (((LoupBeta)livings[i]).getCurrentAction() as A_SeNourrir != null)
                        {
                            if(getAnimal().DEBUG)
                                Debug.Log("Exception! break ..");
                            break;
                        }
                    }
                canEat = ( i == livings.Count );
                if(canEat && getAnimal().DEBUG)
                    Debug.Log("Le loup omega peut se nourrir !");
            }
        }

        if(canEat)
        {
            if(Vector2.Distance(cadavre.transform.position,getAnimal().transform.position) <= .3f)
            {
                if (getAnimal().DEBUG)
                    Debug.Log("mange ...");
                float nourritureMangee = cadavre.mangeCadavre(1);
                if(nourritureMangee == 0)
                {
                    getActionPendlingList().removeAction(this);
                    return true;
                }
                ((Loup)getAnimal()).faim += nourritureMangee;
                if( ((Loup)getAnimal()).faim >= ((Loup)getAnimal()).FAIM_MAX)
                {
                    getActionPendlingList().removeAction(this);
                    return true;
                }
            }
            else
            {
                if (getAnimal().DEBUG)
                    Debug.Log("dirige vers cadavre");
                getAnimal().faceTo(cadavre);
                getAnimal().fd(getAnimal().vitesse,true,false);
            }
        }
        else
        {
            if(Vector2.Distance(cadavre.transform.position,getAnimal().transform.position) <= 3)
            {
                if (getAnimal().DEBUG)
                    Debug.Log("s'éloigne des autres qui mangent");
                getAnimal().faceTo(cadavre);
                getAnimal().rt(180);
                getAnimal().fd();
            }
            else if (getAnimal().DEBUG)
                Debug.Log("Attend son tour pour manger");
        }

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj as A_SeNourrir != null)
        {
            return true;// ((A_SeNourrir)obj).cadavre == cadavre;
        }
        return false;
    }
}