using UnityEngine;
using System.Collections;

public class MindLoupInferieur : MindLoup
{
    private float time = 0;
    private float timeBeforeCheckAlpha = Random.Range(8,15);
    private float chronoBeforeFollowingAlpha = -1;

    public MindLoupInferieur(LoupInferieur agent)
        : base(agent)
    { }
    public override void vivre()
    {
        Animal a = (Animal)agent;
        time += Time.deltaTime;
        if (LoupInferieur.alpha.getCurrentAction() as AU_MoveTo != null)
        {
            if (chronoBeforeFollowingAlpha == -1 )
            {
                chronoBeforeFollowingAlpha = (float)Random.Range(13, 50) * .1f;
            }
           
            if (chronoBeforeFollowingAlpha > 0 )
            {
                chronoBeforeFollowingAlpha -= Time.deltaTime;
                if (chronoBeforeFollowingAlpha <= 0)
                {
                    chronoBeforeFollowingAlpha = -1;
                    actionList.addAction(new AU_FollowAlpha());
                }
            }
        }

        if (!a.perceptView.getLiving().Contains(LoupInferieur.alpha) && time >= timeBeforeCheckAlpha)
        {
            time -= timeBeforeCheckAlpha;
            timeBeforeCheckAlpha = Random.Range(8,15);
            actionList.addAction(new A_RejoindreTroupe());
        }
        base.vivre();
    }

    protected override void randomAction()
    {
        if (Random.Range(1, 3) == 1)
            actionList.addAction(new A_AffectionAuLoupAlpha());
        else
            base.randomAction();
    }

}