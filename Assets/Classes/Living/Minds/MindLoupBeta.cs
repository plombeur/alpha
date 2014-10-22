using UnityEngine;
using System.Collections;

public class MindLoupBeta : MindLoup
{
    private float time = 0;

    public MindLoupBeta(LoupBeta agent)
        : base(agent)
    { }
    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupBeta.vivre ...");
        Animal a = (Animal) agent;
        time += Time.deltaTime;
        if (a.perceptView.getLiving().Count == 0&&time>=10)
        {
            time -= 10;
            actionList.addAction(new A_RejoindreTroupe());
        }
        base.vivre();
    }

}