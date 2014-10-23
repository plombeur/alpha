using UnityEngine;
using System.Collections;

public class MindLoupBeta : MindLoup
{
    private float time = 0;
    private float timeBeforeCheckAlpha = 0;//Random.Range(40,90);

    public MindLoupBeta(LoupBeta agent)
        : base(agent)
    { }
    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupBeta.vivre ...");
        Animal a = (Animal) agent;
        time += Time.deltaTime;
        LoupAlpha alpha = GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();
        if (!a.perceptView.getLiving().Contains(alpha)&&time>=timeBeforeCheckAlpha)
        {
            time -= timeBeforeCheckAlpha;
            timeBeforeCheckAlpha = Random.Range(40, 90);
            actionList.addAction(new A_RejoindreTroupe());
        }
        base.vivre();
    }

}