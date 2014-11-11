﻿using UnityEngine;
using System.Collections;

public class MindLoupInferieur : MindLoup
{
    private float time = 0;
    private float timeBeforeCheckAlpha = Random.Range(8,15);

    public MindLoupInferieur(LoupInferieur agent)
        : base(agent)
    { }
    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupInferieur.vivre ...");
        Animal a = (Animal)agent;
        time += Time.deltaTime;
        LoupAlpha alpha = GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();
        if (alpha.getCurrentAction() as AU_MoveTo != null)
        {
            actionList.addAction(new AU_FollowAlpha());
        }

        if (!a.perceptView.getLiving().Contains(alpha) && time >= timeBeforeCheckAlpha)
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