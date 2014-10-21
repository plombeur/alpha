using UnityEngine;
using System.Collections;

public class A_RejoindreTroupe : Action
{
    private float vitesse;
    private float cptNouvelleTrajectoire = 0;
    private float time = 0;

    public A_RejoindreTroupe()
        : base("A_RejoindreTroupe")
    {}

    public override float getPriority()
    {
        return 1;
    }

    protected override bool onUpdate(float deltaTime)
    {
        if (Living.DEBUG)
            Debug.Log("A_RejoindreTroupe ...");
        /**** CODE A MODIFIER QUAND LES PERCEPTS SERONT FONCTIONNELS *****/
        GameObject obj = GameObject.Find("AlphaWolf");
        Vector2 posAlpha = obj.GetComponent<Transform>().position;
        Vector2 posThis = getAnimal().GetComponent<Transform>().position;
        float dist = Mathf.Sqrt(Mathf.Pow(posAlpha.x - posThis.x, 2) + Mathf.Pow(posAlpha.y - posThis.y, 2));
        if (Living.DEBUG)
            Debug.Log("dist from loup alpha: " + dist);
        /*****************************************************************/
        if (dist <= 10)
        {
            getActionPendlingList().removeAction(this);
        }
        else
        {
            PerceptView percepts = getAnimal().GetComponent<PerceptView>();
            if (percepts != null && percepts.getLiving().Count > 0)
            {
                getAnimal().fd(Animal.VITESSE * 2);
            }
            else
            {
                getAnimal().rt(1);
                getAnimal().fd(0.01f);
            }
        }
        
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_RejoindreTroupe action = obj as A_RejoindreTroupe;
        return action != null;
    }
}
