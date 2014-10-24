using UnityEngine;
using System.Collections;

public class MindLoupInferieur : MindLoup
{
    private float time = 0;
    private float timeBeforeCheckAlpha = Random.Range(15,60);

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
        if (!a.perceptView.getLiving().Contains(alpha) && time >= timeBeforeCheckAlpha)
        {
            time -= timeBeforeCheckAlpha;
            timeBeforeCheckAlpha = Random.Range(15, 60);
            actionList.addAction(new A_RejoindreTroupe());
        }
        base.vivre();
    }

}