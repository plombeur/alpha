using UnityEngine;
using System.Collections;

public class MindLoupOmega : MindLoup
{
    private float time = 0;

    public MindLoupOmega(LoupOmega agent)
        : base(agent)
    { }

    public override void vivre()
    {
        if (Living.DEBUG)
            Debug.Log("MindLoupOmega.vivre ...");
        Animal a = (Animal)agent;
        time += Time.deltaTime;
        if (a.perceptView.getLiving().Count == 0 && time >= 10)
        {
            time -= 10;
            actionList.addAction(new A_RejoindreTroupe());
        }
        base.vivre();
    }

}